namespace Vebinar_nasledovanie
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Zoo animals = new Zoo(20);
            animals.DoInspection();
        }
    }

    class Zoo
    {
        Animal[] animal;
        Random rand = new Random();

        public Zoo(int size)
        {
            animal = new Animal[size];
            GetRandomAnimals();
        }
        public void DoInspection()
        {
            Console.WriteLine("#  Animal    | Feed at      | Health State | S. life Days |Hungry ");
            Console.WriteLine("----------------------------------------------------------------");
            for (int i = 0; i < animal.Length; i++)
            {
                string p = $"{i + 1})";
                if (p.Length < 3)
                    p += " ";
                p += ($"{animal[i].Name}");

                AddSpaces(ref p, 9 - animal[i].Name.Length);
                p += " | ";
                p += animal[i].DeliveryTimestamp.ToString("MMM dd, yyyy");
                p += " | ";
                p += ($"{animal[i].HealthState}");
                AddSpaces(ref p, 12 - animal[i].HealthState.Length);
                p += " | ";
                AddSpaces(ref p, 12 - animal[i].StorageLifeDays.ToString().Length);
                p += $"{animal[i].StorageLifeDays.ToString()} |";
                p += animal[i].IsHungry();
                Console.WriteLine(p);
            }

        }
        void AddSpaces(ref string p, int diff)
        {
            if (diff > 0)
            {
                for (int ii = 0; ii < diff; ii++)
                    p += " ";
            }
        }
        void GetRandomAnimals()
        {

            for (int i = 0; i < animal.Length; i++)
            {
                int random_number = rand.Next(1, 6);
                Animal Animals;
                switch (random_number)
                {
                    case 1:
                        Animals = new Lion("Lion");
                        break;
                    case 2:
                        Animals = new Camel("Camel");
                        break;
                    case 3:
                        Animals = new Jiraf("Jiraf");
                        break;
                    case 4:
                        Animals = new Crocodile("Crocodile");
                        break;
                    default:
                        Animals = new Monkey("Monkey");
                        break;
                }

                Animals.DeliveryTimestamp = GetRandomFeedTime();
                Animals.HealthState = GetRandomHealthState();
                animal[i] = Animals;
            }

        }

        string GetRandomHealthState()
        {
            string health = "Healthy";
            int random_number = rand.Next(1, 3);
            if (random_number == 1)
                health = "Ill";
            return health;
        }
        DateTime GetRandomFeedTime()
        {
            int random_number = rand.Next(1, 200);
            DateTime deliveryTimestamp = DateTime.Now.AddDays(-random_number);
            return deliveryTimestamp;
        }
    }

    class Animal
    {
        string _name;
        protected double LifeDays;
        protected DateTime deliveryTimestamp;
        protected string healthState;


        public Animal(string name)
        {
            _name = name;
        }
        public virtual string Name
        {
            get { return _name; }
            //set { _name = value; }
        }
        public virtual DateTime DeliveryTimestamp
        {
            get { return deliveryTimestamp; }
            set { deliveryTimestamp = value; }
        }
        public virtual string HealthState
        {
            get { return healthState; }
            set { healthState = value; }
        }
        public virtual double StorageLifeDays
        {
            get
            { return LifeDays; }
            set { LifeDays = value; }
        }
        public virtual bool IsHungry()
        {
            if (DateTime.Now.AddDays(-LifeDays) > deliveryTimestamp)
                return false;
            return true;
        }
    }

    class Lion : Animal
    {
        public Lion(string name) : base(name)
        {
            LifeDays = 2;
        }

        public override bool IsHungry()
        {
            double lifeDays = LifeDays;
            if (healthState == "Ill")
                lifeDays = lifeDays / 2;

            if (DateTime.Now.AddDays(-lifeDays) > deliveryTimestamp)
                return false;
            return true;
        }
    }

    class Camel : Animal
    {
        public Camel(string name) : base(name)
        {
            LifeDays = Double.PositiveInfinity;
        }

        public override bool IsHungry()
        {
            return true;
        }
    }

    class Jiraf : Animal
    {
        public Jiraf(string name) : base(name)
        {
            LifeDays = 10;
        }

        public override bool IsHungry()
        {
            double lifeDays = LifeDays;
            if (healthState == "Ill")
                lifeDays = lifeDays / 5;

            if (DateTime.Now.AddDays(-lifeDays) > deliveryTimestamp)
                return false;
            return true;
        }
    }

    class Crocodile : Animal
    {
        public Crocodile(string name) : base(name)
        {
            LifeDays = 150;
        }
    }
    class Monkey : Animal
    {
        public Monkey(string name) : base(name)
        {
            LifeDays = 50;
        }
    }

}