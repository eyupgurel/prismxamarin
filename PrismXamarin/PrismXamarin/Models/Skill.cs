using System;
using System.Collections.Generic;
using System.Text;

namespace PrismXamarin.Models
{
    public class Skill
    {
        private string _name;
        private string _surname;

        public string Surname
        {
            get => _surname;
            set => _surname = value;
        }

        private bool isRequired;

        public string Name
        {
            get => _name;
            set => _name = value;
        }



        public bool IsRequired
        {
            get => isRequired;
            set => isRequired = value;
        }
    }
}
