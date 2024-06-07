"use client";
import CollectionCard from "../CollectionCard";
import { TypographyH2 } from "../common/Typography";
import { useGetTrendingCollectionsQuery } from "@/store/api";
import Error from "../common/Error";
import NoData from "../common/NoData";
import TrendingCollectionsShimmers from "../common/shimmers/TrendingCollectionsShimmers";

type Props = {};

const TrendingCollectionsSection = (props: Props) => {
  const {
    data: collections,
    isLoading,
    isError,
    refetch,
  } = useGetTrendingCollectionsQuery({
    page: 1,
    size: 6,
  });
  return (
    <div className="flex flex-col gap-5">
      <TypographyH2 text="Trending Collections" />
      <div className="grid grid-cols-12 justify-center items-center gap-5">
        {isLoading ? (
          <TrendingCollectionsShimmers elements={8} />
        ) : isError ? (
          <Error retry={refetch} />
        ) : collections && collections.length > 0 ? (
          collections.map((collection, index) => (
            <CollectionCard key={index} collection={collection} />
          ))
        ) : (
          <NoData message="No trending collections" />
        )}
      </div>
    </div>
  );
};

export default TrendingCollectionsSection;
