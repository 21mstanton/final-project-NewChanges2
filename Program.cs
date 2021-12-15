//final project
//Max Stanton
// october 25th, 2021

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace final_project
{
    class Program
    {
        //string that takes the patients file and turns it into a list
        public static List<string> patients = new List<string>();
        //this takes the contents from the file and turns it into a string
        static List<string> GetPatientFileContents(string fileName)
        {
            var fileContents = File.ReadAllLines("patients.txt");
            return fileContents.ToList();
        }

        public static List<string> insuranceInfo = new List<string>();

        static List<string> GetInsuranceFileContetns(string fileName)
        {
            var fileContents = File.ReadAllLines("patientInsuranceInfo.txt");
            return fileContents.ToList();
        }

  
        //method for getting valid string input.
        static string validStringInput()
        {
            while (true)
            {
                string input = Console.ReadLine().ToLower();
                if (input == "edit" || input == "add" || input == "cancel")
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("please enter a valid command.");
                }
            }
        }
        private static void DisplayPatientFileContents()
        {
            Console.WriteLine("\n=================================================");
            for (int i = 0; i < patients.Count; i++)
            {
                Console.WriteLine(patients[i]);
            }
            Console.WriteLine("=================================================\n");
        }

         private static void DisplayInsuranceInfoFileContents()
        {
            Console.WriteLine("\n=======================================================");
            for (int i = 0; i < insuranceInfo.Count; i++)
            {
                Console.WriteLine(insuranceInfo[i]);
            }
            Console.WriteLine("=======================================================\n");
        }

        //this method is number input varification.
        static int ReadInput(int min, int max, String errorMessage = "Please enter a valid number.")
        {
            while (true)
            {

                var input = Console.ReadLine();
                try
                {
                    var numInput = int.Parse(input);
                    if (numInput >= min && numInput <= max)
                    {
                        return numInput;
                    }
                    Console.WriteLine($"Please make sure your value is between {min} and {max}.");
                }
                catch
                {
                    Console.WriteLine(errorMessage);
                }
            }
        }

        //this method actually checks the patient information.
        static bool VarifyingPatentAccess()
        {
            TopOfPass:
            //different passwords for accessing patient info.
            int insuranceInfoPass = 623454;
            Console.WriteLine("please enter a password to access patient information that is 6 integers long.");
            int passcodeinput = ReadInput(1, 623454);

            while (true)
            {

                if (passcodeinput == insuranceInfoPass)
                {
                    Console.WriteLine("password succsess!");
                    bool accessGranted = true;
                    return accessGranted;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("please enter a valid password");
                     Console.ResetColor();
                    goto TopOfPass;
                }
            }
        }

        static DateTime ValidDate()
        {
           System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-au");
           Console.WriteLine("Enter a date: ");
           var temp = Console.ReadLine();
           DateTime userDateInput;
           DateTime.TryParse(temp, out userDateInput);
        if(!userDateInput.Equals(DateTime.MinValue))
        {
            Console.WriteLine("please enter a valid date format");
        }
            return userDateInput;
        }
        static void Main(string[] args)
        {
            //this puts the insurance info file contents into a insuracninfo variable.
            insuranceInfo = GetInsuranceFileContetns("patientInsuranceInfo.txt");
            //this puts the patient file contents into a patients variable
            patients = GetPatientFileContents("patients.txt");
            while (true)
            {
            //for a goto function to go back to the main menu from edit application. and the alarm application.
            ToMainMenu:
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("patient directory");
                Console.ResetColor();
                DisplayPatientFileContents();

                //this is to actually know if the user wants to access patient inf or edit patient info.
                Console.WriteLine("--Welcome! please enter an input by typing a number that corrisponds with your use of the program.--\n");
                Console.WriteLine(" 1: adding patient info.\n 2: for saving info.\n 3: setting up alarm for patient medicine intaken\n 4: access or edit patient insurance information.");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" 5: for canceling out of the program.");
                Console.ResetColor();
                var userInput = ReadInput(1, 5);
                Console.WriteLine("\n");

                //allows you to brows and edit patient info.
                if (userInput == 1)
                {
                topOfEdit:
                    //adding a password to access patient information
                    Console.Clear();
                    DisplayPatientFileContents();
                    Console.WriteLine("adding information...");
                    Console.WriteLine("if you would like to add patient info type 'add'. to edit already existing info type the word 'edit'.\n");
                    Console.WriteLine("--if you would like to exit this application type the word: cancel--");
                    var editInput = validStringInput();

                    while (true)
                    {
                        if (editInput == "edit")
                        {
                            Console.Clear();
                            DisplayPatientFileContents();
                            Console.WriteLine("Which index would you like to edit?");
                            int intInput = ReadInput(1, patients.Count);
                            Console.WriteLine($"please enter your input for index {intInput}");
                            Console.WriteLine("the format is: name | medicine");
                            var specificEditInput = Console.ReadLine();
                            patients.Remove(patients[intInput - 1]);
                            patients.Insert(intInput - 1, $"{intInput}. {specificEditInput}");
                            goto topOfEdit;
                        }
                        if (editInput == "add")
                        {
                            Console.Clear();
                            DisplayPatientFileContents();
                            Console.WriteLine("Please enter your input for the next index.");
                            Console.WriteLine("the format is: name | medicine");
                            string addInput = Console.ReadLine();
                            patients.Add($"{patients.Count + 1}. {addInput}");
                            goto topOfEdit;
                        }
                        if (editInput == "cancel")
                        {
                            goto ToMainMenu;
                        }
                    }
                }

                // this allows you to brows and read patient info.
                if (userInput == 2)
                {
                    Console.WriteLine("saving patient information...");
                    File.WriteAllLines("patients.txt", patients);
                    File.WriteAllLines("patientInsuranceInfo.txt", insuranceInfo);
                }

                //allows you to set a reminder of when patients need meds
                
                if (userInput == 3)
                {
                    while(true)
                    {
                        TopOfReminderEdit:
                        Console.Clear();
                        DisplayPatientFileContents();
                        Console.WriteLine(" Please type the word 'add' to add a timer\n If you would like to return to the main menu please type the word 'cancel'.");
                        string stringEditInput =  validStringInput();

                        if(stringEditInput == "add")
                        {
                            Console.Clear();
                            DisplayPatientFileContents();
                            Console.WriteLine("please enter a index number for the associated patient you would like to set a reminder for");
                            int ReminderPatientInput = ReadInput(1, patients.Count);
                            DateTime now = DateTime.Today;
                            Console.WriteLine($"you have selected to set a reminder for {patients[ReminderPatientInput - 1]}");
                            Console.WriteLine("what day would you like to set a reminder for?");
                            Console.WriteLine("the format for the input is: year, month, day. example: 2021, 11, 15 ");
                            DateTime TimeInput = ValidDate();

                            if (TimeInput == now)
                            {
                                Console.WriteLine($"Alert! {patients[ReminderPatientInput - 1]} requires his daily meds.");
                            }
                            goto TopOfReminderEdit;
                        }
                        if (stringEditInput == "cancel")
                        {
                            goto ToMainMenu;
                        }
                    }
                   
                }
                if(userInput == 4)
                {
                    while(true)
                    {
                        Console.WriteLine("accessing insurance info...");
                        var varifyingPass = VarifyingPatentAccess();

                        topOfInsuranceInfo:
                        if(varifyingPass == true)
                        {
                            Console.Clear();
                            DisplayInsuranceInfoFileContents();
                            Console.WriteLine(" Please type the word 'add' to add new patient insurance info.\n type the word 'edit' to edit previously existing patininsurance info.\n type the word -cancel- to cancel to the main menu");
                            var editInsuranceInfoInput = validStringInput();

                            if(editInsuranceInfoInput == "edit")
                            {
                                DisplayInsuranceInfoFileContents();
                                Console.WriteLine("Which index would you like to edit?");
                                int intInput = ReadInput(1, insuranceInfo.Count);
                                Console.WriteLine($"please enter your input for index {intInput}");
                                 Console.WriteLine("the format is: name | medicine | insurance");
                                var specificEditInput = Console.ReadLine();
                                insuranceInfo.Remove(insuranceInfo[intInput - 1]);
                                insuranceInfo.Insert(intInput - 1, $"{intInput}. {specificEditInput}");
                                goto topOfInsuranceInfo;
                            }
                            if(editInsuranceInfoInput == "add")
                            {
                                 Console.Clear();
                                 DisplayPatientFileContents();
                                 Console.WriteLine("Please enter your input for the next index.");
                                  Console.WriteLine("the format is: name | medicine | insurance");
                                 string addInput = Console.ReadLine();
                                 insuranceInfo.Add($"{insuranceInfo.Count + 1}. {addInput}");
                                 goto topOfInsuranceInfo;
                            }
                            if(editInsuranceInfoInput == "cancel")
                            {
                                goto ToMainMenu;
                            }

                        }
                    }

                }
                else if (userInput == 5)
                {
                    return;
                }
            }



        }
    }

}