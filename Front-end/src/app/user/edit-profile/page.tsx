import { TypographyH2 } from "@/components/common/Typography";
import EditProfileForm from "@/components/user/EditProfileForm";
type Props = {};

const page = (props: Props) => {
  return (
    <div className="flex flex-col gap-8 max-w-[500px] mx-auto mt-8 lg:mt-16">
      <div className="flex flex-col gap-3 border-b pb-5">
        <TypographyH2 text="Edit Profile" />
      </div>
      <EditProfileForm />
    </div>
  );
};

export default page;
