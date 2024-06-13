"use client";
import NFTDetailsLeft from "@/components/asset/NFTDetailLeft";
import NFTDetailsRight from "@/components/asset/NFTDetailRight";
import NFTProvenance from "@/components/asset/NFTProvenance";
import MoreFromCollection from "@/components/asset/MoreFromCollection";
import MoreFromCreator from "@/components/asset/MoreFromCreator";
import { useGetNFTQuery } from "@/store/api";
import Error from "@/components/common/Error";
type Props = {
  params: { id: string };
};

const NFTDetail = ({ params }: Props) => {
  const {
    data: asset,
    isLoading,
    isError,
  } = useGetNFTQuery(params.id as string);
  return (
    <div className="flex flex-col gap-10 pt-10">
      {isError ? (
        <div className="flex justify-center items-center w-full h-[50vh]">
          <Error />
        </div>
      ) : (
        <div className="flex flex-col lg:flex-row gap-10">
          <NFTDetailsLeft asset={asset} isLoading={isLoading} />
          <NFTDetailsRight asset={asset} isLoading={isLoading} />
        </div>
      )}
      <NFTProvenance tokenId={asset?.tokenId as number} />
      {asset?.collection && <MoreFromCollection id={asset?.collection?.id} />}
      <MoreFromCreator address={asset?.creator?.address} />
    </div>
  );
};

export default NFTDetail;
