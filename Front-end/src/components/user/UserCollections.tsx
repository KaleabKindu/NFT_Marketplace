import React from "react";
import CollectionCard from "../CollectionCard";
import { collections } from "@/utils";
type Props = {};

const UserCollections = (props: Props) => {
  return (
    <div className="grid grid-cols-1  md:grid-cols-2 lg:grid-cols-3 justify-center items-center gap-5">
      {collections.map((collection, index) => (
        <CollectionCard key={index} collection={collection} />
      ))}
    </div>
  );
};

export default UserCollections;
