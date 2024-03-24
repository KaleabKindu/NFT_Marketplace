import { collections } from "@/utils";
import CollectionCard from "../CollectionCard";
import { TypographyH2 } from "../common/Typography";

type Props = {};

const TrendingCollectionsSection = (props: Props) => {
  return (
    <div className="flex flex-col gap-5">
      <TypographyH2 text="Trending Collections" />
      <div className="grid grid-cols-1  md:grid-cols-2 lg:grid-cols-3 justify-center items-center gap-5">
        {collections.slice(0, 6).map((collection, index) => (
          <CollectionCard key={index} collection={collection} />
        ))}
      </div>
    </div>
  );
};

export default TrendingCollectionsSection;
