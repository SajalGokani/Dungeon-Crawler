using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;

namespace Assessment_1_Dungeon_Crawler
{
    /*Create a class called Character that has the following properties:
        Name (string) 
        Class (string) 
        Level (int) 
        HitPoints (int) 
        AvailableAttributePoints (int)
        Skills (List<Skill>)
     */
    public class Character
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public int Level { get; set; }
        public int HitPoints { get; set; }
        public int AvailableAttributePoints { get; set; }
        public List<Skill> Skills { get; set; }

        //Set level up function to allow the user to increase the level, add an empty skills list so it is not null
        public Character(int startingLevel = 1)
        {
            Level = startingLevel;
            Skills = new List<Skill>();
        }
        //Level up function levels up a character and add 10 hit points to the character
        public void LevelUp()
        {
            Level++;
            HitPoints = HitPoints + 5;
            AvailableAttributePoints = AvailableAttributePoints + 10;
        }

        public override String ToString()
        {
            return $"Name: {this.Name}, " +
                     $"Class: {this.Class}, " +
                     $"Level: {this.Level}, " +
                     $"HitPoints: {this.HitPoints}, " +
                     $"Available Attribute Points: {this.AvailableAttributePoints}";
        }
     }

    /*Create a class called Skill that has the following properties:
        Name (string) 
        Description (string) 
        Attribute (string)
        RequiredAttributePoints (int)
     */
    public class Skill
    {
        //Create an id so you can find the skill using the id when you check to see if a character has enough attriibute points for a skill
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Attribute { get; set; }
        public int RequiredAttributePoints { get; set; }

        //Adding a bool as a parameter so the id can be returned or not
        public String ToString(bool includeId)
        {
            string skill = $"{this.Name} - {this.Description} - {this.Attribute} - Point Requirement:{this.RequiredAttributePoints}";
            if (includeId)
            {
                skill = $"{this.Id}. {skill}";
            }
            return skill;
        }
    }


    internal class Program
    {
        //Function: To create a character that takes the list of characters as a parameter 
        public static void createCharacter(List<Character> characters)
        {
            Character newCharacter = new Character();

            Console.Write("Enter name: ");
            newCharacter.Name = Console.ReadLine();

            Console.Write("Enter class: ");
            newCharacter.Class = Console.ReadLine();

            Console.Write("Enter Total Attribute Points: ");
            newCharacter.AvailableAttributePoints = int.Parse(Console.ReadLine());

            newCharacter.Level = 1;
            newCharacter.HitPoints = 10 + (newCharacter.AvailableAttributePoints / 2);

            characters.Add(newCharacter);
        }

        //Function to assign a skill to a character
        public static void assignASkill (List<Character> characters, List <Skill> listSkills)
        {

            Console.Write("Enter character name: ");
            string characterAssignSkill = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine();

            Character characterToAssignSkill = characters.Find(c => c.Name.Equals(characterAssignSkill, StringComparison.OrdinalIgnoreCase));

            Console.WriteLine($"Total Attribute Points Available for this character: {characterToAssignSkill.AvailableAttributePoints}");
            Console.WriteLine("Available skills: ");

            foreach (Skill skill in listSkills)
            {                
                Console.WriteLine(skill.ToString(true));
            }
            int skillChoice = 0;
            Skill skillToAssign = null;
            Console.Write("Select a skill to assign: ");
            while (skillChoice == 0)
            {
                try
                {
                    skillChoice = int.Parse(Console.ReadLine());
                    skillToAssign = listSkills.Find(s => s.Id == skillChoice);
                    if (skillToAssign == null)
                    {
                        Console.WriteLine("Invalid selection. Please enter a number in range (1..3):");
                        skillChoice = 0;
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid selection. Please enter a number in range (1..3):");
                }

            }

            skillToAssign = listSkills.Find(s => s.Id == skillChoice);


            if (skillToAssign.RequiredAttributePoints > characterToAssignSkill.AvailableAttributePoints)
            {
                Console.WriteLine("Not enough attribute points are available!");
            }
            else if (characterToAssignSkill.Skills.Contains(skillToAssign))
            {
                Console.WriteLine($"{characterToAssignSkill.Name} already has {skillToAssign.Name} skills!");
            }
            else
            {
                int charactersPoints = characterToAssignSkill.AvailableAttributePoints;
                int requiredPoints = skillToAssign.RequiredAttributePoints;
                int remainingPoints = charactersPoints - requiredPoints;

                characterToAssignSkill.AvailableAttributePoints = remainingPoints;
                characterToAssignSkill.Skills.Add(skillToAssign);

                Console.WriteLine($"Skill: {skillToAssign.Name} added to {characterToAssignSkill.Name}");

            }
        }

        //Function: To level up a character that takes the list of characters as a parameter 
        public static void characterLevelUp(List<Character> characters)
        {
            Console.Write("Enter character name: ");
            string characterLevelUp = Console.ReadLine();

            Character characterToLevel = characters.Find(c => c.Name.Equals(characterLevelUp, StringComparison.OrdinalIgnoreCase));

            if (characterToLevel != null)
            {
                characterToLevel.LevelUp();
                Console.WriteLine($"{characterToLevel.Name} is now a Level:{characterToLevel.Level} chracter.");
            }
            else
            {
                Console.WriteLine("Character not found!");
            }
        }

        //Function to Display Character information
        public static void displayCharacters (List<Character> characters)
        {
            Console.WriteLine("All Characters in the character sheet.......................");

            foreach (Character c in characters)
            {
                Console.WriteLine(c.ToString());

                if (c.Skills == null || c.Skills.Count == 0)
                {
                    Console.WriteLine("Skills: ");
                    Console.WriteLine("There are no skills assigned yet...!");
                }
                else
                {
                    Console.WriteLine("Skills:");
                    foreach (Skill skill in c.Skills)
                    {
                        Console.WriteLine(skill.ToString(false));
                    }
                }
                Console.WriteLine("\n");
            }
            Console.WriteLine("End.........................................................\n");
        }

        static void Main(string[] args)
        {
            //The Main method declares a list of Character objects to store the characters and a list of Skill objects to store the available skills in the game.
            //change the name of the list "skills" to "listSkills" so its easier to differentiate in switch case #2 
            List<Character> characters = new List<Character>();
            List<Skill> listSkills = new List<Skill>

            //useable code from assignment one:
            {
                new Skill { Id = 1, Name = "Strike", Description = "A powerful strike.", Attribute = "Strength", RequiredAttributePoints=10 },
                new Skill { Id = 2, Name = "Dodge", Description = "Avoid an attack.", Attribute = "Dexterity", RequiredAttributePoints=15 },
                new Skill { Id = 3, Name = "Spellcast", Description = "Cast a spell.", Attribute = "Intelligence", RequiredAttributePoints=20 }
            };

            //Main menu
            bool menu = true;
            while (menu) 
            {
            Console.WriteLine("Main Menu:");
            Console.WriteLine("1. Create a charater");
            Console.WriteLine("2. Assign skills");
            Console.WriteLine("3. Level up a character");
            Console.WriteLine("4. Display all character sheets");
            Console.WriteLine("5. Exit");

            Console.Write("Enter your choice: ");
            int choice=int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:                
                        createCharacter(characters);
                        break;

                    case 2:
                        assignASkill(characters, listSkills);
                        break;

                    case 3:
                        characterLevelUp(characters);
                        break;

                    case 4:
                        displayCharacters(characters);
                        break;

                    case 5:
                        Console.WriteLine("Exting the program. Goodbye!");
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid input. Please pick an option from the menu!");
                        Console.WriteLine();
                        break;
                }
            }
        }
    }
}
