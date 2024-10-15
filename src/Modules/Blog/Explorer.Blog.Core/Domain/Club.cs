using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain
{
    public class Club : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ImageDirectory {  get; private set; }
        public Club()
        {
            Name = string.Empty;
            Description = string.Empty;
            ImageDirectory = string.Empty;
        }

        public Club(string name, string description, string imageDirectory) 
        { 
            Name = name; 
            Description = description; 
            ImageDirectory = imageDirectory; 
        }
    }
}
