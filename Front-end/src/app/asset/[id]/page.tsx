"use client";
import NFTDetailsLeft from "@/components/asset/NFTDetailLeft";
import NFTDetailsRight from "@/components/asset/NFTDetailRight";
import NFTProvenance from "@/components/asset/NFTProvenance";
import MoreFromCollection from "@/components/asset/MoreFromCollection";
import MoreFromCreator from "@/components/asset/MoreFromCreator";
import { useGetNFTQuery } from "@/store/api";
import Error from "@/components/common/Error";
import { SocketContext } from "@/context/SocketContext";
import { useContext, useEffect } from "react";
type Props = {
  params: { id: string };
};

const NFTDetail = ({ params }: Props) => {
  const {
    data: asset,
    isLoading,
    isError,
    refetch,
  } = useGetNFTQuery(params.id as string);
  const { socketConnection } = useContext(SocketContext);
  useEffect(() => {
    socketConnection?.on(`RefetchAsset${asset?.id}`, () => {
      refetch()
    })
    return () =>{
      socketConnection?.off(`RefetchAsset${asset?.id}`)
    }
  }, [socketConnection])
  return (
    <div className="flex flex-col gap-10 pt-10">
      {isError ? (
        <div className="flex justify-center items-center w-full h-[50vh]">
          <Error retry={refetch} />
        </div>
      ) : (
        <div className="flex flex-col lg:flex-row gap-10">
          <NFTDetailsLeft asset={asset} isLoading={isLoading} />
          <NFTDetailsRight asset={asset} isLoading={isLoading} />
        </div>
      )}
      <NFTProvenance id={asset?.id as number} />
      {asset?.collection && <MoreFromCollection id={asset?.collection?.id} />}
      <MoreFromCreator address={asset?.creator?.address} />
    </div>
  );
};

export default NFTDetail;
