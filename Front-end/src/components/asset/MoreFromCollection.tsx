"use client";

import {
  Accordion,
  AccordionContent,
  AccordionItem,
  AccordionTrigger,
} from "@/components/ui/accordion";
import NFTCard from "../explore/assets/NFTCard";
import { ScrollArea, ScrollBar } from "../ui/scroll-area";
import { useState, useEffect } from "react";
import { NFT } from "@/types";
import { useGetAssetsQuery } from "@/store/api";
import AssetsShimmers from "../common/shimmers/AssetsShimmers";
import NoData from "../common/NoData";
import Error from "../common/Error";

type Props = {
  id?: number;
};
const MoreFromCollection = ({ id }: Props) => {
  const { data, isLoading, isError, refetch } = useGetAssetsQuery({
    collectionId: id?.toString(),
    pageNumber: 1,
    pageSize: 12,
  });
  const [assets, setAssets] = useState<NFT[]>([]);
  useEffect(() => {
    if (data) {
      setAssets([...data.value]);
    }
  }, [data]);
  return (
    <Accordion type="single" collapsible defaultValue="item-1">
      <AccordionItem value="item-1">
        <AccordionTrigger className="bg-accent text-accent-foreground px-5 rounded-t-md mb-5">
          More From Collection
        </AccordionTrigger>
        <AccordionContent>
          <ScrollArea>
            <div className="flex gap-5">
              {isLoading ? (
                <AssetsShimmers elements={12} className="w-[400px]" />
              ) : isError ? (
                <Error retry={refetch} />
              ) : assets && assets.length > 0 ? (
                assets.map((asset, index) => (
                  <div key={index} className="shrink-0 w-[400px]">
                    <NFTCard asset={asset} />
                  </div>
                ))
              ) : (
                <NoData message="No assets found" />
              )}
            </div>
            <ScrollBar className="hidden" orientation="horizontal" />
          </ScrollArea>
        </AccordionContent>
      </AccordionItem>
    </Accordion>
  );
};

export default MoreFromCollection;
