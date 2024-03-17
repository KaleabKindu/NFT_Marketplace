import React from "react";
import { Creator } from "../landing-page/TopCreators";
import { users } from "@/utils";

type Props = {};

const UserFollowersFollowings = (props: Props) => {
  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 items-center gap-5">
      {users.map((user, index) => (
        <Creator key={index} user={user} index={index} />
      ))}
    </div>
  );
};

export default UserFollowersFollowings;
