import CollectionDetail from "@/components/collection/CollectionDetail";
import Filters from "@/components/collection/Filters";
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
      <Filters />
      <CollectionNFTs />
    </div>
  );
};

export default Page;
