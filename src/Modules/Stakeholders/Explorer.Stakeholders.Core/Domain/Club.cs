﻿using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Explorer.Stakeholders.API.Dtos;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Club : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ImageDirectory {  get; private set; }
        public long OwnerId { get; private set; }
        public List<ClubMessage> ClubMessages { get; private set; } = new List<ClubMessage>();
        public Club(string name, string description, string imageDirectory,long ownerId) 
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            Name = name; 
            Description = description; 
            ImageDirectory = imageDirectory;
            OwnerId = ownerId;
            ClubMessages = new List<ClubMessage>();
            Validate();
        }

        public void AddMessage(ClubMessage message)
        {
            ClubMessages.Add(message);
            Validate();
        }
     
        public void RemoveMessage(long messageId)
        {
            var message = ClubMessages.FirstOrDefault(m => m.Id == messageId);
            if (message != null)
            {
                ClubMessages.Remove(message);
                Validate();
            }
        }

        public void UpdateMessage(long messageId, string newContent)
        {
            var message = ClubMessages.FirstOrDefault(m => m.Id == messageId);
            if(message != null)
            {
                message.SetContent(newContent);
                Validate();
            }
        }

        public List<ClubMessage> GetClubMessages()
        {
            return ClubMessages;
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentException("Club name cannot be empty.");

            ClubMessages.ForEach(m => m.Validate());
        }
    }
}
