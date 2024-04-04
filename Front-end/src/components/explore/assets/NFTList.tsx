"use client";
import { assets as assetsData } from "@/utils";
import NFTCard from "./NFTCard";
import NoData from "@/components/common/NoData";
import Error from "@/components/common/Error";
import { useGetAssetsQuery } from "@/store/api";
import AssetsShimmers from "@/components/common/shimmers/AssetsShimmers";
import { useSearchParams } from "next/navigation";
import { useEffect, useState } from "react";
import { NFT } from "@/types";
import Pagination from "@/components/common/Pagination";
type Props = {};

const NFTList = (props: Props) => {
  const params = useSearchParams();
  const [page, setPage] = useState(1);
  const [total, setTotal] = useState(0);
  const [size, setSize] = useState(12);
  const { data, isFetching, isError } = useGetAssetsQuery({
    filter: params.toString(),
    page: page,
    size: size,
  });
  const [assets, setAssets] = useState<NFT[]>(assetsData);
  useEffect(() => {
    if (data) {
      setAssets([...assets, ...data.value]);
      setTotal(data.count);
    }
  }, [data]);
  return (
    <>
      <div className="grid grid-cols-12 items-center justify-center gap-5">
        {isFetching ? (
          <AssetsShimmers elements={size} />
        ) : isError ? (
          <Error />
        ) : assets && assets.length > 0 ? (
          <>
            {assets.slice(0, size).map((asset, index) => (
              <NFTCard key={index} asset={asset} />
            ))}
            <Pagination
              total={100}
              currentPage={page}
              setPage={(a: number) => {
                setPage(a);
                window.scrollTo({ top: 0, behavior: "smooth" });
              }}
            />
          </>
        ) : (
          <NoData message="No assets found" />
        )}
      </div>
    </>
  );
};

export default NFTList;
