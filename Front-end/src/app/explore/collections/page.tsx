import Filters from "@/components/explore/collections/Filters";
import CollectionsList from "@/components/explore/collections/CollectionsList";
import { Metadata } from "next/types";

type Props = {};
export const metadata: Metadata = {
  title: "Explore All Collections | NFT Marketplace",
};

const Page = (props: Props) => {
  return (
    <div className="flex flex-col gap-10">
      <Filters />
      <CollectionsList />
    </div>
  );
};

export default Page;
