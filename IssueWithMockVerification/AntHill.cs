using System;

namespace IssueWithMockVerification
{
    public class AntHill
    {
        private readonly IOutsideWorld outsideWorld;

        public AntHill(IOutsideWorld outsideWorld)
        {
            this.outsideWorld = outsideWorld;
        }

        public void SendOutTwoSoldierAnts()
        {
            var soldierAnt = new Ant { Type = "Soldier Ant" };

            SendOutforFood(soldierAnt, 1);
            SendOutforFood(soldierAnt, 2);
        }

        private void SendOutforFood(Ant ant, int id)
        {
            ant.Id = id;
            Console.WriteLine($"Ant: Id = {ant.Id}, Type = {ant.Type}");
            outsideWorld.SendOut(ant);
        }
    }

    public class Ant
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }
}