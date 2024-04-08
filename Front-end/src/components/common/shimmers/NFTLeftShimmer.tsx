import { Skeleton } from "@/components/ui/skeleton";
import React from "react";

type Props = {};

const NFTLeftShimmer = (props: Props) => {
  return <Skeleton className="flex-1 h-[25rem] lg:h-[50rem] rounded-3xl" />;
};

export default NFTLeftShimmer;
