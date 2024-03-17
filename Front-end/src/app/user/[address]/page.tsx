"use client";
import UserDetail from "@/components/user/UserDetail";
import UserNFTs from "@/components/user/UserNFTs";
import { useState } from "react";
import UserCollections from "@/components/user/UserCollections";
import UserFollowersFollowings from "@/components/user/UserFollowersFollowings";
import PopularUsers from "@/components/user/PopularUsers";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";

type Props = {
  params: {
    address: string;
  };
};

const Page = ({ params }: Props) => {
  return (
    <div className="flex flex-col gap-10">
      <UserDetail address={params.address} />
      <Tabs defaultValue="created">
        <TabsList className="flex lg:w-[50%] w-full mb-10">
          <TabsTrigger className="flex-1" value="created">
            Created
          </TabsTrigger>
          <TabsTrigger className="flex-1" value="collections">
            Collections
          </TabsTrigger>
          <TabsTrigger className="flex-1" value="followers">
            Followers
          </TabsTrigger>
          <TabsTrigger className="flex-1" value="following">
            Following
          </TabsTrigger>
        </TabsList>
        <TabsContent value="created">
          <UserNFTs />
        </TabsContent>
        <TabsContent value="collections">
          <UserNFTs />
        </TabsContent>
        <TabsContent value="followers">
          <UserFollowersFollowings />
        </TabsContent>
        <TabsContent value="following">
          <UserFollowersFollowings />
        </TabsContent>
      </Tabs>
      <PopularUsers />
    </div>
  );
};

export default Page;
