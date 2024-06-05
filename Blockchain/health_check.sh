#!/bin/bash

# Perform the health check using curl and capture the HTTP response body
response=$(curl -s -X POST --data '{"jsonrpc":"2.0","method":"eth_getCode","params":["0x5FbDB2315678afecb367f032d93F642f64180aa3", "latest"],"id":1}' http://127.0.0.1:8545)

# Print the response body
echo "===============================> Response: $response"

# Extract the "result" field from the JSON response using bash string manipulation
result=$(echo "$response" | grep -o '"result":"[^"]*' | sed 's/"result":"//')
echo "===============================> Response Body: $result"

# Check if the result contains "0x"
if [[ -z "$result" || "$result" == "0x" ]]; then
  echo "Health check failed, exit with status 1 (failure)"
  exit 1
else  
  echo "Health check passed, exit with status 0 (success)"
  exit 0 
fi
