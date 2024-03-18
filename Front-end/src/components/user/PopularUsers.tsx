import { users } from "@/utils";
import { TypographyH2 } from "../common/Typography";
import { Creator } from "../landing-page/TopCreators";

type Props = {};

const PopularUsers = (props: Props) => {
  return (
    <div className="flex flex-col gap-5 mt-12">
      <TypographyH2 text="Popular Users" />
      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 items-center gap-5">
        {users.slice(0, 8).map((user, index) => (
          <Creator key={index} user={user} index={index} />
        ))}
      </div>
    </div>
  );
};

export default PopularUsers;
