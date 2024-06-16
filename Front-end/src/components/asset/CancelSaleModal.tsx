import { useState } from "react";
import { TypographyP } from "../common/Typography";
import { Button } from "../ui/button";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { Loader2 } from "lucide-react";
import { useCancelAssetSaleMutation } from "@/store/api";

type CancelSaleModalProps = {
  id: number;
};
export const CancelSaleModal = ({ id }: CancelSaleModalProps) => {
  const [open, setOpen] = useState(false);
  const [cancelSale, { isLoading }] = useCancelAssetSaleMutation();
  const handleClose = () => setOpen(false);
  const handleCancel = async () => {
    try {
      await cancelSale(id as number).unwrap();
    } catch (error) {
      console.log("error", error);
    }
  };

  return (
    <Dialog open={open} onOpenChange={(a) => setOpen(a)}>
      <DialogTrigger asChild>
        <Button type="button" className="flex-1 lg:w-[50%] bg-red-500 w-full">
          Cancel Sale
        </Button>
      </DialogTrigger>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Cancel Sale</DialogTitle>
          <DialogDescription className="flex flex-col gap-5 pt-5">
            <div className="flex flex-col gap-5">
              <div>
                <TypographyP
                  className="text-base text-red-500"
                  text="Are you sure you want to cancel sale?"
                />
              </div>
              <div className="flex justify-end gap-5">
                <Button
                  type="button"
                  variant={"ghost"}
                  className="rounded-full self-end"
                  size="lg"
                >
                  No
                </Button>
                <Button
                  type="button"
                  disabled={isLoading}
                  className="rounded-full self-end"
                  size="lg"
                  onClick={handleCancel}
                >
                  {isLoading ? (
                    <Loader2 className="mr-2 h-6 w-6 animate-spin" />
                  ) : (
                    "Yes"
                  )}
                </Button>
              </div>
            </div>
          </DialogDescription>
        </DialogHeader>
      </DialogContent>
    </Dialog>
  );
};
