import { ethers } from "hardhat";

async function main() {

  const store = await ethers.deployContract("Store");

  await store.waitForDeployment();

  console.log(
    `Deployed to address ${store.target}`
  );
}

// We recommend this pattern to be able to use async/await everywhere
// and properly handle errors.
main().catch((error) => {
  console.error(error);
  process.exitCode = 1;
});
