import NFTList from "@/components/explore/assets/NFTList";
import Filters from "@/components/explore/assets/Filters";
import { Metadata } from "next/types";
import { SearchInput } from "@/components/common/SearchFilters";

type Props = {};
export const metadata: Metadata = {
  title: "Explore All NFTS | NFT Marketplace",
};

const Page = (props: Props) => {
  return (
    <div className="flex flex-col gap-10">
      <SearchInput className="mt-[2vh] md:w-[60%] w-full mx-auto" />
      <Filters />
      <NFTList />
    </div>
  );
};

export default Page;
