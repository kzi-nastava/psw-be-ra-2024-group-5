#!/bin/bash

MODULES_PATH="src/Modules/"

# Remove migrations folders
rm -rf $MODULES_PATH/Stakeholders/Explorer.Stakeholders.Infrastructure/Migrations
rm -rf $MODULES_PATH/Tours/Explorer.Tours.Infrastructure/Migrations
rm -rf $MODULES_PATH/Blog/Explorer.Blog.Infrastructure/Migrations

# For StakeholdersContext
dotnet ef migrations add Init -c StakeholdersContext -p $MODULES_PATH/Stakeholders/Explorer.Stakeholders.Infrastructure -s src/Explorer.API
dotnet ef database update -c StakeholdersContext -p $MODULES_PATH/Stakeholders/Explorer.Stakeholders.Infrastructure -s src/Explorer.API

# For ToursContext
dotnet ef migrations add Init -c ToursContext -p $MODULES_PATH/Tours/Explorer.Tours.Infrastructure -s src/Explorer.API
dotnet ef database update -c ToursContext -p $MODULES_PATH/Tours/Explorer.Tours.Infrastructure -s src/Explorer.API

# # For BlogContext
dotnet ef migrations add Init -c BlogContext -p $MODULES_PATH/Blog/Explorer.Blog.Infrastructure -s src/Explorer.API
dotnet ef database update -c BlogContext -p $MODULES_PATH/Blog/Explorer.Blog.Infrastructure -s src/Explorer.API
