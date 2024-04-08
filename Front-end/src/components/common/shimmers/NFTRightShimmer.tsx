import { Skeleton } from "@/components/ui/skeleton";
import React from "react";

type Props = {};

const NFTRightShimmer = (props: Props) => {
  return (
    <div className="flex-1 flex flex-col gap-16 p-3">
      <Skeleton className="w-[90%] h-12" />
      <div className="flex flex-wrap w-full gap-5">
        <div className="flex-1 flex gap-3 items-center">
          <Skeleton className="w-24 h-16 rounded-full" />
          <div className="flex flex-col w-full gap-2">
            <Skeleton className="w-full h-6" />
            <Skeleton className="w-full h-6" />
          </div>
        </div>
        <div className="flex-1 flex gap-3 items-center">
          <Skeleton className="w-24 h-16 rounded-full" />
          <div className="flex flex-col w-full gap-2">
            <Skeleton className="w-full h-6" />
            <Skeleton className="w-full h-6" />
          </div>
        </div>
        <div className="flex-1 flex gap-3 items-center">
          <Skeleton className="w-24 h-16 rounded-full" />
          <div className="flex flex-col w-full gap-2">
            <Skeleton className="w-full h-6" />
            <Skeleton className="w-full h-6" />
          </div>
        </div>
      </div>
      <div className="flex flex-col gap-5">
        <Skeleton className="w-[50%] h-10 " />
        <Skeleton className="w-[70%] h-10 " />
        <Skeleton className="w-[80%] h-10 " />
      </div>
      <div className="flex flex-col gap-5">
        <Skeleton className="w-[30%] h-8 " />
        <Skeleton className="w-[50%] h-8 " />
        <Skeleton className="w-[60%] h-10 " />
      </div>
      <Skeleton className="flex-1 w-full h-auto " />
    </div>
  );
};

export default NFTRightShimmer;
