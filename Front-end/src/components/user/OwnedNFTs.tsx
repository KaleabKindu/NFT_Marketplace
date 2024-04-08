"use client";
import { assets as assetsData } from "@/utils";
import NFTCard from "../explore/assets/NFTCard";
import { useInView } from "react-intersection-observer";
import { useState, useEffect } from "react";
import { NFT } from "@/types";
import { useGetAssetsQuery } from "@/store/api";
import AssetsShimmers from "../common/shimmers/AssetsShimmers";
import NoData from "../common/NoData";
import Error from "../common/Error";
import { useParams } from "next/navigation";

type Props = {};

const OwnedNFTs = (props: Props) => {
  const params = useParams();
  const [page, setPage] = useState(1);
  const [total, setTotal] = useState(0);
  const [size, setSize] = useState(12);
  const { data, isLoading, isFetching, isError } = useGetAssetsQuery({
    owner: params.address as string,
    pageNumber: page,
    pageSize: size,
  });
  const [assets, setAssets] = useState<NFT[]>(assetsData);
  const { ref, inView } = useInView({ threshold: 0.3 });
  useEffect(() => {
    if (data) {
      setAssets([...assets, ...data.value]);
      setTotal(data.count);
    }
  }, [data]);
  useEffect(() => {
    if (inView && !(page * size >= total)) {
      setPage(page + 1);
    }
  }, [inView]);
  return (
    <>
      <div className="grid grid-cols-12 items-center justify-center gap-5">
        {isLoading ? (
          <AssetsShimmers elements={size} />
        ) : isError ? (
          <Error />
        ) : assets && assets.length > 0 ? (
          <>
            {assets.slice(0, size).map((asset, index) => (
              <NFTCard key={index} asset={asset} />
            ))}
            {isFetching && <AssetsShimmers elements={size} />}
          </>
        ) : (
          <NoData message="No assets found" />
        )}
      </div>
      <div ref={ref} />
    </>
  );
};

export default OwnedNFTs;
