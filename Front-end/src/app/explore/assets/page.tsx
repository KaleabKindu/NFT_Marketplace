import NFTList from "@/components/explore/assets/NFTList";
import Filters from "@/components/explore/assets/Filters";
import { Metadata } from "next/types";
import { SearchInput } from "@/components/common/SearchFilters";
import { MdArrowForward } from "react-icons/md";

type Props = {};
export const metadata: Metadata = {
  title: "Explore All NFTS | NFT Marketplace",
};

const Page = (props: Props) => {
  return (
    <div className="flex flex-col gap-10">
      <SearchInput
        className="mt-[2vh] w-[60%] mx-auto"
        postIcon={<MdArrowForward size={30} />}
      />
      <Filters />
      <NFTList />
    </div>
  );
};

export default Page;
