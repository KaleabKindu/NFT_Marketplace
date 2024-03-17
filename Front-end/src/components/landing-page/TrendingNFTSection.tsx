import TrendingNFTCard from "./TrendingNFTCard";
import { TypographyH2, TypographyH4 } from "../common/Typography";
import { assets } from "@/utils";
type Props = {};

const TrendingNFTSection = (props: Props) => {
  return (
    <div className="flex flex-col gap-5">
      <TypographyH2 text="Discover More NFTs" />
      <TypographyH4 text="Explore New Trending NFTs" />
      <div className="grid grid-cols-1  md:grid-cols-2 lg:grid-cols-3 items-center gap-5">
        {assets.slice(0, 6).map((asset, index) => (
          <TrendingNFTCard asset={asset} key={index} />
        ))}
      </div>
    </div>
  );
};

export default TrendingNFTSection;
