import CollectionDetail from "@/components/collection/CollectionDetail";
import SearchFilter from "@/components/collection/SearchFilter";
import TopCollections from "@/components/collection/TopCollections";
import CollectionNFTs from "@/components/collection/CollectionNFTs";

type Props = {
  params: {
    id: string;
  };
};

const Page = ({ params }: Props) => {
  return (
    <div className="flex flex-col gap-10">
      <CollectionDetail id={params.id} />
      <SearchFilter />
      <CollectionNFTs />
      <TopCollections />
    </div>
  );
};

export default Page;
