"use client";
import NFTDetailsLeft from "@/components/asset/NFTDetailLeft";
import NFTDetailsRight from "@/components/asset/NFTDetailRight";
import NFTProvenance from "@/components/asset/NFTProvenance";
import MoreFromCollection from "@/components/asset/MoreFromCollection";
import MoreFromCreator from "@/components/asset/MoreFromCreator";
import { useGetNFTQuery } from "@/store/api";
type Props = {
  params: { id: string };
};

const NFTDetail = ({ params }: Props) => {
  const { data: asset, isLoading } = useGetNFTQuery(params.id as string);
  return (
    <div className="flex flex-col gap-10 pt-10">
      <div className="flex flex-col lg:flex-row gap-10">
        <NFTDetailsLeft asset={asset} isLoading={isLoading} />
        <NFTDetailsRight asset={asset} isLoading={isLoading} />
      </div>
      <NFTProvenance />
      {asset?.collection && <MoreFromCollection id={asset?.collection?.id} />}
      <MoreFromCreator address={asset?.creator?.address} />
    </div>
  );
};

export default NFTDetail;
