"use client";
import { categories } from "@/data";
import Image from "next/image";
import { Card, CardContent, CardFooter } from "@/components/ui/card";
import {
  TypographyH2,
  TypographyH4,
  TypographySmall,
} from "../common/Typography";
import Link from "next/link";
import { Routes } from "@/routes";
import { IconType } from "react-icons";
import { Badge } from "../ui/badge";
import { useGetCategoryCountQuery } from "@/store/api";
import CategoriesShimmers from "../common/shimmers/CategoriesShimmers";
import NoData from "../common/NoData";
type Props = {};

const BrowseCategorySection = (props: Props) => {
  const { data: count, isLoading, isError } = useGetCategoryCountQuery();
  return (
    <div className="flex flex-col gap-5">
      <TypographyH2 text={"Browse By Category"} />
      <div className="grid grid-cols-1 xs:grid-cols-2 md:grid-cols-3 lg:grid-cols-5 gap-5">
        {isLoading ? (
          <CategoriesShimmers elements={10} />
        ) : categories && categories.length > 0 ? (
          categories.map((category, index) => {
            const Icon = categories.find((cat) => cat.value === category.value)
              ?.icon as IconType;
            return (
              <Link
                key={index}
                href={`${Routes.NFTS}?categories=${category.value}`}
              >
                <Card className="hover:scale-110 rounded-2xl">
                  <CardContent className="flex gap-1 items-center py-2 px-3">
                    <Badge className="p-2 bg-background/30 hover:bg-background text-foreground">
                      <Icon size={30} />
                    </Badge>
                    <div className="flex flex-col items-start gap-2 px-5">
                      <TypographyH4
                        className="text-primary/80"
                        text={category.name}
                      />
                      <TypographySmall
                        className="text-primary/50"
                        text={`${count?.[category.value]} NFTs`}
                      />
                    </div>
                  </CardContent>
                  <CardFooter className="relative h-44 p-0">
                    <Image
                      className=" object-cover rounded-b-2xl"
                      src={`/landing-page/${category.image}`}
                      fill
                      alt=""
                    />
                  </CardFooter>
                </Card>
              </Link>
            );
          })
        ) : (
          <NoData message="No asset categories." />
        )}
      </div>
    </div>
  );
};

export default BrowseCategorySection;
