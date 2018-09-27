# IssueWithMockVerification

I have this situation where my code is working as expected. But when I try to verify the code through a mock, the implementation seems to behave differently. Below an explanation of the problem, and if you clone this repository, you can find a failing unit test which reproduces the issue.

The project is a default C# .NET Core unit test project with the Moq mocking library added.

Given the following code:

```
public void SendOutTwoSoldierAnts()
{
    var soldierAnt = new Ant { Type = "Soldier Ant" };

    SendOutforFood(soldierAnt, 1);
    SendOutforFood(soldierAnt, 2);
}

private void SendOutforFood(Ant ant, int id)
{
    ant.Id = id;
    Console.WriteLine($"Ant: Id {ant.Id}, Type = {ant.Type}");
    outsideWorld.SendOut(ant);
}
```

## Running the code works as expected

Two ants are send out for food, each with a unique id. The following line:
```
Console.WriteLine($"Ant: Id {ant.Id}, Type = {ant.Type}");
```
Procuces following outputs:

`Ant: Id = 1, Type = Soldier Ant`

`Ant: Id = 2, Type = Soldier Ant`

The code is working as expected, as each ant has a different id.

## Verification of this code through a mock fails

When I try to assert with the following line:
```
outsideWorldMock.Verify(m => m.SendOut(It.Is<Ant>(a => a.Id.Equals(1))), Times.Once);
```
The test runner says that no such invocation was done. Which is weird, because when I debug the code I can see that it works as expected.

## Troubleshooting the issue
### Solution 1
When I replace the following code 
```
private void SendOutforFood(Ant ant, int id)
{
    ant.Id = id;
    Console.WriteLine($"Ant: Id {ant.Id}, Type = {ant.Type}");
    outsideWorld.SendOut(ant);
}
```
with this -> _hardcoding the id instead of using the method parameter named id_
```
private void SendOutforFood(Ant ant, int id)
{
    ant.Id = 1;
    Console.WriteLine($"Ant: Id {ant.Id}, Type = {ant.Type}");
    outsideWorld.SendOut(ant);
}
```
Mocking verification works!

So it has something todo with the Ant object that is being passed through, and of which the id is being updated.

### Solution 2
When I replace the following code
```
public void SendOutTwoSoldierAnts()
{
    var soldierAnt = new Ant { Type = "Soldier Ant" };

    SendOutforFood(soldierAnt, 1);
    SendOutforFood(soldierAnt, 2);
}
```
with this -> _creating a new object for each ant_
```
public void SendOutTwoSoldierAnts()
{
    var soldierAnt = new Ant { Type = "Soldier Ant" };

    SendOutforFood(new Ant { Id = 1, Type = "Soldier Ant" });
    SendOutforFood(new Ant { Id = 2, Type = "Soldier Ant" });
}
```
Mocking verification also works.

## Final thoughts

So I can get the code to work. And I know the second solution is a better implementation than the initial one. But I am not seeing the problem here. I'm guessing this is an issue with, or my limited knowledge of, the mocking library Moq.