#!/bin/bash

# exit when any command fails
set -e

echo "Downloading the Database"
curl https://static.isala.me/iit/workflow.db -o workflow.db

echo "Starting Nginx"
service nginx start

echo "Starting the Server"
dotnet WorkFlow.Server.dll