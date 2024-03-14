import CollectionCard from "../CollectionCard";
import { TypographyH2 } from "../common/Typography";

type Props = {};

const TrendingCollectionsSection = (props: Props) => {
  return (
    <div className="flex flex-col gap-5">
      <TypographyH2 text="Trending Collections" />
      <div className="grid grid-cols-1  md:grid-cols-2 lg:grid-cols-3 justify-center items-center gap-5">
        {Array.from({ length: 6 }).map((_, index) => (
          <CollectionCard key={index} />
        ))}
      </div>
    </div>
  );
};

export default TrendingCollectionsSection;
