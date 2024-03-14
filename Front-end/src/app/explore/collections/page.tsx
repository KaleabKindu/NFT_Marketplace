import CollectionsList from "@/components/explore/collections/CollectionsList";
import SearchFilter from "@/components/explore/collections/SearchFilter";
import { Metadata } from "next/types";

type Props = {};
export const metadata: Metadata = {
  title: "Explore All Collections | NFT Marketplace",
};

const Page = (props: Props) => {
  return (
    <div className="flex flex-col gap-10 pt-16">
      <SearchFilter />
      <CollectionsList />
    </div>
  );
};

export default Page;
