"use client";

import {
  Accordion,
  AccordionContent,
  AccordionItem,
  AccordionTrigger,
} from "@/components/ui/accordion";
import NFTCard from "../explore/assets/NFTCard";
import { ScrollArea, ScrollBar } from "../ui/scroll-area";
import { assets as assetsData } from "@/utils";
import { useState, useEffect } from "react";
import { NFT } from "@/types";
import { useGetAssetsQuery } from "@/store/api";
import AssetsShimmers from "../common/shimmers/AssetsShimmers";
import NoData from "../common/NoData";
import Error from "../common/Error";
import { useParams } from "next/navigation";

type Props = {
  address?: string;
};
const MoreFromCreator = ({ address }: Props) => {
  const { data, isLoading, isError } = useGetAssetsQuery({
    creator: address as string,
    pageNumber: 1,
    pageSize: 12,
  });
  const [assets, setAssets] = useState<NFT[]>(assetsData);
  useEffect(() => {
    if (data) {
      setAssets([...assets, ...data.value]);
    }
  }, [data]);
  return (
    <Accordion type="single" collapsible defaultValue="item-1">
      <AccordionItem value="item-1">
        <AccordionTrigger className="bg-accent text-accent-foreground px-5 rounded-t-md mb-5">
          More From Creator
        </AccordionTrigger>
        <AccordionContent>
          <ScrollArea>
            <div className="flex gap-5">
              {isLoading ? (
                <AssetsShimmers elements={12} className="w-[400px]" />
              ) : isError ? (
                <Error />
              ) : assets && assets.length > 0 ? (
                assets.slice(0, 12).map((asset, index) => (
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

export default MoreFromCreator;
