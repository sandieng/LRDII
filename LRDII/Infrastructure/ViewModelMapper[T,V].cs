using LRDII.Models;

namespace LRDII.Infrastructure
{
    public class ViewModelMapper
    {
        public static V MapViewModelToModel<T, V>(T transactionVM, V outTransactionModel) where V : class, new()
        {
            if (outTransactionModel.GetType() == typeof(ShareTransactionModel))
            {
                var outTransaction = outTransactionModel as ShareTransactionModel;
                var transaction = transactionVM as ShareTransactionViewModel;
                var multiplier = 1;
                outTransaction.NomorAnggota = transaction.NomorAnggota;
                outTransaction.NomorHargaSaham = transaction.NomorHargaSaham;
                outTransaction.JenisTransaksi = transaction.JenisTransaksi;
                outTransaction.TanggalTransaksi = transaction.TanggalTransaksi;

                if (transaction.JenisTransaksi == ShareTransactionType.JualSaham)
                    multiplier = -1;

                outTransaction.JumlahSaham = transaction.JumlahSaham * multiplier;

                return outTransaction as V;
            }

            if (outTransactionModel.GetType() == typeof(EditShareTransactionViewModel))
            {
                var outTransaction = outTransactionModel as EditShareTransactionViewModel;
                var transaction = transactionVM as ShareTransactionModel;
                outTransaction.NomorAnggota = transaction.NomorAnggota;
                outTransaction.NomorHargaSaham = transaction.NomorHargaSaham;
                outTransaction.JumlahSaham = transaction.JumlahSaham;
                outTransaction.TanggalTransaksi = transaction.TanggalTransaksi;
                outTransaction.NomorTransaksi = transaction.NomorTransaksi;

                return outTransaction as V;
            }

            if (outTransactionModel.GetType() == typeof(LoanTransactionModel))
            {
                var transaction = transactionVM as LoanTransactionViewModel;

                var outTransaction = outTransactionModel as LoanTransactionModel;
                outTransaction.NomorAnggota = transaction.NomorAnggota;
                outTransaction.TanggalTransaksi = transaction.TanggalTransaksi;
                outTransaction.PersentaseBunga = transaction.PersentaseBunga;
                outTransaction.LamaPinjaman = transaction.LamaPinjaman;
                outTransaction.JumlahPinjaman = transaction.JumlahPinjaman;

                return outTransaction as V;
            }

            if (outTransactionModel.GetType() == typeof(LoanRepaymentTransactionModel))
            {
                var transaction = transactionVM as LoanTransactionViewModel;

                var outTransaction = outTransactionModel as LoanRepaymentTransactionModel;
                outTransaction.NomorAnggota = transaction.NomorAnggota;
                outTransaction.NomorPinjaman = transaction.NomorPinjaman;
                outTransaction.TanggalTransaksi = transaction.TanggalTransaksi;
                outTransaction.JumlahBungaPinjaman = transaction.JumlahBungaPinjaman;
                outTransaction.JumlahPinjamanPokok = transaction.JumlahPinjamanPokok;

                return outTransaction as V;
            }

            if (outTransactionModel.GetType() == typeof(GoodsTransactionModel))
            {
                var outTransaction = outTransactionModel as GoodsTransactionModel;
                var transaction = transactionVM as GoodsTransactionViewModel;
                outTransaction.NomorAnggota = transaction.NomorAnggota;
                outTransaction.JenisTransaksi = transaction.JenisTransaksi;
                outTransaction.TanggalTransaksi = transaction.TanggalTransaksi;
                outTransaction.HargaBarangJasa = transaction.HargaBarangJasa;

                return outTransaction as V;
            }

            return null;
        }
    }
}
