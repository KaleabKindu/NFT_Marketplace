"use client";
import { assets } from "@/utils";
import NFTCard from "../explore/assets/NFTCard";
type Props = {};

const CreatedNFTS = (props: Props) => {
  return (
    <div className="grid grid-cols-12 items-center justify-center gap-5">
      {assets.map((asset, index) => (
        <NFTCard key={index} asset={asset} />
      ))}
    </div>
  );
};

export default CreatedNFTS;
