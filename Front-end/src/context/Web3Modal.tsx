"use client";
import { createWeb3Modal, defaultWagmiConfig } from "@web3modal/wagmi/react";

import { Chain, WagmiConfig } from "wagmi";
import { ReactNode } from "react";
import {
  arbitrum,
  avalanche,
  bsc,
  fantom,
  localhost,
  gnosis,
  mainnet,
  optimism,
  polygon,
} from "wagmi/chains";

const awsnode: Chain = {
  id: 1337,
  name: "nft-gebeya",
  network: "nft-gebeya",
  nativeCurrency: {
    name: "Ether",
    symbol: "ETH",
    decimals: 18,
  },
  rpcUrls: {
    default: {
      http: ["https://nodes.nft-gebeya.com"],
    },
    public: {
      http: ["https://nodes.nft-gebeya.com"],
    },
  },
  testnet: true,
};

const chains = [
  mainnet,
  polygon,
  avalanche,
  awsnode,
  arbitrum,
  bsc,
  optimism,
  gnosis,
  fantom,
];

const projectId = process.env.NEXT_PUBLIC_PROJECT_ID || "";
const metadata = {
  name: "NFT Marketplace",
  description:
    "The Largest NFT Marketplace to Buy, Sell and Discover Exclusive Digital Products as Non-Fungible Token(NFTs)",
  url: "https://web3modal.com",
  icons: ["https://avatars.githubusercontent.com/u/37784886"],
};

const wagmiConfig = defaultWagmiConfig({ chains, projectId, metadata });

createWeb3Modal({ wagmiConfig, projectId, chains });

type Props = {
  children: ReactNode;
};
export function Web3Modal({ children }: Props) {
  return <WagmiConfig config={wagmiConfig}>{children}</WagmiConfig>;
}
