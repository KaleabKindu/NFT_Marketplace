"use client";
import TrendingNFTCard from "./TrendingNFTCard";
import { TypographyH2, TypographyH4 } from "../common/Typography";
import { useGetTrendingAssetsQuery } from "@/store/api";
import TrendingNFTShimmers from "../common/shimmers/TrendingNFTShimmers";
import Error from "../common/Error";
import NoData from "../common/NoData";
type Props = {};

const TrendingNFTSection = (props: Props) => {
  const {
    data: assets,
    isLoading,
    isError,
    refetch,
  } = useGetTrendingAssetsQuery({
    page: 1,
    size: 6,
  });
  return (
    <div className="flex flex-col gap-5">
      <TypographyH2 text="Discover More NFTs" />
      <TypographyH4 text="Explore New Trending NFTs" />
      <div className="grid grid-cols-1  md:grid-cols-2 lg:grid-cols-3 items-center gap-5">
        {isLoading ? (
          <TrendingNFTShimmers elements={6} />
        ) : isError ? (
          <Error retry={refetch} />
        ) : assets && assets.length > 0 ? (
          assets.map((asset, index) => (
            <TrendingNFTCard asset={asset} key={index} />
          ))
        ) : (
          <NoData message="No assets found" />
        )}
      </div>
    </div>
  );
};

export default TrendingNFTSection;
