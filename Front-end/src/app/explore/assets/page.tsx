import NFTList from "@/components/explore/assets/NFTList";
import SearchBar from "@/components/explore/assets/SearchBar";
import Filter from "@/components/explore/assets/Filter";
import { Metadata } from "next/types";

type Props = {};
export const metadata: Metadata = {
  title: "Explore All NFTS | NFT Marketplace",
};

const Page = (props: Props) => {
  return (
    <div className="flex flex-col gap-10">
      <SearchBar className="mt-[10vh] w-[60%] mx-auto" />
      <Filter />
      <NFTList />
    </div>
  );
};

export default Page;
