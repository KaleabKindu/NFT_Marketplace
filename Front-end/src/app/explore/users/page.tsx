import { Metadata } from "next/types";
import UsersList from "@/components/explore/users/UsersList";
import { SearchInput } from "@/components/common/SearchFilters";
export const metadata: Metadata = {
  title: "Explore All Users | NFT Marketplace",
};

type Props = {};

const Page = (props: Props) => {
  return (
    <div className="flex flex-col gap-10">
      <div className="w-full max-w-3xl mx-auto">
        <SearchInput />
      </div>
      <UsersList />
    </div>
  );
};

export default Page;
