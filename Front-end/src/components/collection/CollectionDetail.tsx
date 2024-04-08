"use client";
import Image from "next/image";
import {
  TypographyH2,
  TypographyH3,
  TypographyP,
  TypographySmall,
} from "../common/Typography";
import { collections } from "@/utils";
import { useGetCollectionDetailsQuery } from "@/store/api";
import { Skeleton } from "../ui/skeleton";

type Props = {
  id: string;
};

const CollectionDetail = ({ id }: Props) => {
  const { data, isLoading } = useGetCollectionDetailsQuery(id);
  const collection = collections.find((collection) => collection.id === id);
  return (
    <div className="relative flex flex-col lg:flex-row gap-8 -mt-[15vh] w-[90%] lg:w-[85%] mx-auto bg-background border z-40 rounded-3xl p-8">
      {isLoading ? (
        <Skeleton className="w-full h-80 rounded-3xl" />
      ) : (
        <>
          <div className="relative w-full h-[300px] lg:w-[350px] lg:h-[300px]">
            <Image
              className="rounded-3xl object-cover"
              src={collection?.avatar || "/collection/collection-pic.png"}
              fill
              alt=""
            />
          </div>
          <div className="flex flex-col gap-5">
            <TypographyH2
              className="capitalize"
              text={collection?.name || "Awesome NFT Collection"}
            />
            <TypographyP
              text={
                collection?.description ||
                "Karafuru is home to 5,555 generative arts where colors reign supreme. Leave the drab reality and enter the world of Karafuru by Museum of Toys."
              }
            />
            <div className="grid grid-cols-2 lg:grid-cols-4 gap-4">
              <div className="flex flex-col items-center gap-5 lg:gap-8 p-5 lg:p-7 border rounded-xl">
                <TypographySmall
                  className="text-accent-foreground/60"
                  text="Floor Price"
                />
                <TypographyH3
                  className="text-accent-foreground/60"
                  text={`${parseFloat(collection?.floor_price as string).toFixed(2)} ETH`}
                />
              </div>
              <div className="flex flex-col items-center gap-5 lg:gap-8 p-5 lg:p-7 border rounded-xl">
                <TypographySmall
                  className="text-accent-foreground/60"
                  text="Volume"
                />
                <TypographyH3
                  className="text-accent-foreground/60"
                  text={`${parseFloat(collection?.volume as string).toFixed(2)} ETH`}
                />
              </div>
              <div className="flex flex-col items-center gap-5 lg:gap-8 p-5 lg:p-7 border rounded-xl">
                <TypographySmall
                  className="text-accent-foreground/60"
                  text="Latest Price"
                />
                <TypographyH3
                  className="text-accent-foreground/60"
                  text={`${parseFloat(collection?.latest_price as string).toFixed(2)} ETH`}
                />
              </div>
              <div className="flex flex-col items-center gap-5 lg:gap-8 p-5 lg:p-7 border rounded-xl">
                <TypographySmall
                  className="text-accent-foreground/60"
                  text="Items"
                />
                <TypographyH3
                  className="text-accent-foreground/60"
                  text={collection?.items}
                />
              </div>
            </div>
          </div>
        </>
      )}
    </div>
  );
};

export default CollectionDetail;
