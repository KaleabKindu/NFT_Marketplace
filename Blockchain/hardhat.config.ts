import { HardhatUserConfig } from "hardhat/config";
import "@nomicfoundation/hardhat-toolbox";

const INFURA_API_KEY = "2ea683adc71a4651973c4f4a62024e42";

const SEPOLIA_PRIVATE_KEY = "88c17eafdf3ffd3f88f49f7fe024d851abef8207ba08e65d6577c4b000bb19ba";

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
      url: `https://sepolia.infura.io/v3/${INFURA_API_KEY}`,
      accounts: [SEPOLIA_PRIVATE_KEY]
    }
  }
};

export default config;
