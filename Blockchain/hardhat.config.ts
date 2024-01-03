import { HardhatUserConfig } from "hardhat/config";
import "@nomicfoundation/hardhat-toolbox";

const account_1 = process.env.SEPOLIA_PRIVATE_KEY || ''

const config: HardhatUserConfig = {
  solidity: {
    version: "0.8.20",
    settings: {
      optimizer: {
        enabled: true,
        runs: 200
      }
    }
  },
  networks:{
    hardhat:{
      chainId:1337,
    },
    sepolia: {
      chainId:11155111,
      url: `https://sepolia.infura.io/v3/${process.env.INFURA_API_KEY}`,
      accounts: [account_1]
    }
  }
};

export default config;
