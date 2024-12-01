#!bin/bash

n=24

for((i=1; i<n+1; i++)); do
    name="puzzle$i"
    dotnet sln aoc24.sln add "$name/$name.csproj"
done
