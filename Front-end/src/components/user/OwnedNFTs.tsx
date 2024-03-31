"use client";
import { assets } from "@/utils";
import NFTCard from "../explore/assets/NFTCard";
type Props = {};

const OwnedNFTS = (props: Props) => {
  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 items-center justify-center gap-5">
      {assets.map((asset, index) => (
        <NFTCard key={index} asset={asset} />
      ))}
    </div>
  );
};

export default OwnedNFTS;
