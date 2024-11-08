"use client";
import NFTCard from "../explore/assets/NFTCard";
import { useInView } from "react-intersection-observer";
import { useState, useEffect } from "react";
import { NFT } from "@/types";
import { useGetAssetsQuery, useGetOwnedAssetsQuery } from "@/store/api";
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
  const [fetchingNextPage, setFetchingNextPage] = useState(false);
  const { data, isLoading, isFetching, isError, refetch } =
    useGetOwnedAssetsQuery(params.address as string);
  const [assets, setAssets] = useState<NFT[]>([]);
  const { ref, inView } = useInView({ threshold: 1 });
  useEffect(() => {
    if (data) {
      setAssets([...data.value]);
      setTotal(data.count);
    }
  }, [data]);
  useEffect(() => {
    if (inView && size < total) {
      setSize(size * 2);
      setFetchingNextPage(true);
    }
  }, [inView]);
  return (
    <>
      <div className="grid grid-cols-12 items-center justify-center gap-5">
        {isLoading ? (
          <AssetsShimmers elements={size} />
        ) : isError ? (
          <Error retry={refetch} />
        ) : assets && assets.length > 0 ? (
          <>
            {assets.map((asset, index) => (
              <NFTCard key={index} asset={asset} />
            ))}
            {isFetching && fetchingNextPage && (
              <AssetsShimmers elements={size} />
            )}
            <div ref={ref} />
          </>
        ) : (
          <NoData message="No assets found" />
        )}
      </div>
    </>
  );
};

export default OwnedNFTs;
