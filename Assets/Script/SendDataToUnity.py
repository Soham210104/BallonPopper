import json

# Set the desired values
spawn_interval = 0.4
balloon_force = 40.0

# Create a dictionary to hold the data
data = {
    "spawnInterval": spawn_interval,
    "balloonForce": balloon_force
}

# Convert the dictionary to a JSON string
json_data = json.dumps(data)

# Write the JSON data to a file
with open("unity_data.json", "w") as file:
    file.write(json_data)
