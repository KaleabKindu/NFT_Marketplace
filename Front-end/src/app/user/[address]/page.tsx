"use client";
import UserDetail from "@/components/user/UserDetail";
import Followings from "@/components/user/Followings";
import PopularUsers from "@/components/user/PopularUsers";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import Followers from "@/components/user/Followers";
import UserCollections from "@/components/user/UserCollections";
import CreatedNFTS from "@/components/user/CreatedNFTs";
import OwnedNFTS from "@/components/user/OwnedNFTs";

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
          <TabsTrigger className="flex-1" value="owned">
            Owned
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
          <CreatedNFTS />
        </TabsContent>
        <TabsContent value="owned">
          <OwnedNFTS />
        </TabsContent>
        <TabsContent value="collections">
          <UserCollections />
        </TabsContent>
        <TabsContent value="followers">
          <Followers />
        </TabsContent>
        <TabsContent value="following">
          <Followings />
        </TabsContent>
      </Tabs>
    </div>
  );
};

export default Page;
