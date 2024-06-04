# Use the official Node.js image as a base
FROM node:18-alpine

# Install curl
RUN apk --no-cache add curl

# Set the working directory
WORKDIR /app

# Copy the package.json and yarn.lock files
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of the application files
COPY . .

# Copy the health check script into the container
COPY health_check.sh /usr/local/bin/bc_health_check.sh

# Make the health check script executable
RUN chmod +x /usr/local/bin/bc_health_check.sh

# Expose the Hardhat port (default: 8545)
EXPOSE 8545

# Start the Hardhat node and deploy the contract
CMD ["sh", "-c", "npx hardhat node & sleep 10 && npx hardhat run scripts/deploy.ts --network localhost && tail -f /dev/null"]
