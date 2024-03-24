import { collections } from "@/utils";
import CollectionCard from "../CollectionCard";
import { TypographyH2, TypographyH4 } from "../common/Typography";
import { ScrollArea, ScrollBar } from "@/components/ui/scroll-area";

type Props = {};

const TopCollections = (props: Props) => {
  return (
    <div className="flex flex-col gap-5">
      <TypographyH2 text="Top Collections" />
      <TypographyH4 text="Discover the new creative economy" />
      <ScrollArea>
        <div className="flex w-full gap-5">
          {collections.map((collection, index) => (
            <div key={index} className="shrink-0 w-full md:w-[50%] lg:w-[15%]">
              <CollectionCard collection={collection} />
            </div>
          ))}
        </div>
        <ScrollBar className="hidden" orientation="horizontal" />
      </ScrollArea>
    </div>
  );
};

export default TopCollections;
