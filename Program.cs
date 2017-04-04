using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje3
{
    class Program
    {
        class UrunBilgisi
        {
            public string marka;
            public string model;
            public int miktar;
            public double maliyet;
            public double satısFiyatı;
            public string urunAcıklaması;
        }
        class dugum
        {
            public string urunAdı;
            public ArrayList urunBilgileri = new ArrayList();
            public dugum leftChild;
            public dugum rightChild;
            public void uBilgiEkle(UrunBilgisi urunBilgi)
            {
                urunBilgileri.Add(urunBilgi);
            }
            public void yazdır()
            {
                Console.WriteLine(urunAdı);
                foreach (UrunBilgisi birUrun in urunBilgileri)
                    Console.WriteLine("Marka: " + birUrun.marka + "\nModel: " + birUrun.model + "\nMiktar: " + birUrun.miktar + "\nMaliyet: " + birUrun.maliyet + "\nSatış Fiyatı: " + birUrun.satısFiyatı + "\nUrun Açıklaması: " + birUrun.urunAcıklaması);
            }
        }
        class Urun_agac
        {
            public string kategoriAdı;
            public dugum root;
            public int elemanSay;
            public int urunSay;
            public int duzey;
            public Urun_agac()
            {
                root = null;
            }
            public void urunEkle(dugum birDugum)
            {
                if (root == null)
                    root = birDugum;
                else
                {
                    dugum simdiki = root;
                    dugum onceki;
                    while (true)
                    {
                        onceki = simdiki;
                        if (birDugum.urunAdı.CompareTo(simdiki.urunAdı) == -1)
                        {
                            simdiki = simdiki.leftChild;
                            if (simdiki == null)
                            {
                                onceki.leftChild = birDugum;
                                return;
                            }
                        }
                        else if (birDugum.urunAdı.CompareTo(simdiki.urunAdı) == 1)
                        {
                            simdiki = simdiki.rightChild;
                            if (simdiki == null)
                            {
                                onceki.rightChild = birDugum;
                                return;
                            }
                        }
                    }
                }
            }
            public void urunAramaSilme(dugum localroot, string uAdı, string marka, string model)
            {
                if (localroot != null)
                {
                    if (localroot.urunAdı != uAdı)
                    {
                        urunAramaSilme(localroot.leftChild, uAdı, marka, model);
                        urunAramaSilme(localroot.rightChild, uAdı, marka, model);
                    }
                    else
                    {
                        foreach (UrunBilgisi birUrun in localroot.urunBilgileri)
                        {
                            if (birUrun.marka == marka && birUrun.model == model)
                            {
                                localroot.urunBilgileri.Remove(birUrun);
                                break;
                            }
                        }
                    }
                }
            }
            public void preOrder(dugum localRoot)
            {
                if (localRoot != null)
                {
                    Console.Write(duzey + ". duzeyde ");
                    localRoot.yazdır();
                    duzey++;
                    preOrder(localRoot.leftChild);
                    preOrder(localRoot.rightChild);
                    duzey--;
                }
            }
            public void inOrder(dugum localRoot)
            {
                if (localRoot != null)
                {
                    duzey++;
                    inOrder(localRoot.leftChild);
                    Console.Write(duzey + ". duzeyde ");
                    localRoot.yazdır();
                    inOrder(localRoot.rightChild);
                    duzey--;
                }
            }
            public void postOrder(dugum localRoot)
            {
                if (localRoot != null)
                {
                    duzey++;
                    postOrder(localRoot.leftChild);
                    postOrder(localRoot.rightChild);
                    duzey--;
                    Console.Write(duzey + ". duzeyde ");
                    localRoot.yazdır();
                }
            }
            public void derinlik(dugum birDugum, int d)
            {
                if (birDugum == null)
                    return;
                d++;
                if (d > duzey)
                    duzey = d;
                elemanSay++;
                derinlik(birDugum.leftChild, d);
                derinlik(birDugum.rightChild, d);
            }
            public int urunSayBul(dugum localroot)
            {
                if (localroot != null)
                {
                    urunSay += localroot.urunBilgileri.Count;
                    urunSayBul(localroot.leftChild);
                    urunSayBul(localroot.rightChild);
                }
                return urunSay;
            }
        }
        class Heap
        {
            private UrunBilgisi[] heapArray;
            private int maxSize;
            private int currentSize;
            public Heap(int max)
            {
                maxSize = max;
                currentSize = 0;
                heapArray = new UrunBilgisi[maxSize];
            }
            public bool insert(UrunBilgisi birUrun)
            {
                if (currentSize == maxSize)
                    return false;
                UrunBilgisi newNode = birUrun;
                heapArray[currentSize] = newNode;
                trickleUp(currentSize++);
                return true;
            }
            public void trickleUp(int index)
            {
                int parent = (index - 1) / 2;
                UrunBilgisi bottom = heapArray[index];
                while (index > 0 && heapArray[parent].satısFiyatı > bottom.satısFiyatı)
                {
                    heapArray[index] = heapArray[parent];
                    index = parent;
                    parent = (parent - 1) / 2;
                }
                heapArray[index] = bottom;
            }
            public UrunBilgisi remove()
            {
                UrunBilgisi root = heapArray[0];
                heapArray[0] = heapArray[--currentSize];
                trickleDown(0);
                return root;
            }
            public void trickleDown(int index)
            {
                int largerChild;
                UrunBilgisi top = heapArray[index];
                while (index < currentSize / 2)
                {
                    int leftChild = 2 * index + 1;
                    int rightChild = leftChild + 1;
                    if (rightChild < currentSize && heapArray[leftChild].satısFiyatı > heapArray[rightChild].satısFiyatı)
                        largerChild = rightChild;
                    else
                        largerChild = leftChild;
                    if (top.satısFiyatı <= heapArray[largerChild].satısFiyatı)
                        break;
                    heapArray[index] = heapArray[largerChild];
                    index = largerChild;
                }
                heapArray[index] = top;
            }
            public UrunBilgisi displayHeap()
            {
                UrunBilgisi birUrun;
                birUrun = remove();
                Console.WriteLine("Marka: " + birUrun.marka + "\nModel: " + birUrun.model + "\nMiktar: " + birUrun.miktar + "\nMaliyet: " + birUrun.maliyet + "\nSatış Fiyatı: " + birUrun.satısFiyatı + "\nUrun Açıklaması: " + birUrun.urunAcıklaması);
                return birUrun;
            }
        }
        class SanalMarket
        {
            public ArrayList Kategoriler;
            public Hashtable hash = new Hashtable();
            public SanalMarket()
            {
                Kategoriler = new ArrayList();

            }
            public void kategoriEkle(Urun_agac birKategori)
            {
                if (Kategoriler.Count == 0)
                    Kategoriler.Add(birKategori);
                else
                {
                    foreach (Urun_agac birAgac in Kategoriler)
                    {
                        if (birAgac.kategoriAdı == birKategori.kategoriAdı)
                        {
                            birAgac.urunEkle(birKategori.root);
                            return;
                        }
                    }
                    Kategoriler.Add(birKategori);
                }
            }
            public void uBilgiDegistir()
            {
                int cevap, miktar;
                double fiyat;
                string kategoriAdı, marka, model, urunAdi;
                Console.WriteLine("1. Kategori\n2. Marka\n3. Model\n4. Miktar\n5. Fiyat");
                Console.WriteLine("Değiştirmek istediğiniz ürün bilgisini seçiniz: ");
                cevap = int.Parse(Console.ReadLine());
                switch (cevap)
                {
                    case 1:
                        Console.WriteLine("Değiştirmek istediğiniz kategori adını giriniz: ");
                        kategoriAdı = Console.ReadLine();
                        foreach (Urun_agac birAgac in Kategoriler)
                        {
                            if (birAgac.kategoriAdı == kategoriAdı)
                            {
                                Console.WriteLine("Yeni kategori adını giriniz: ");
                                kategoriAdı = Console.ReadLine();
                                birAgac.kategoriAdı = kategoriAdı;
                                break;
                            }
                        }
                        break;
                    case 2:
                        Console.WriteLine("Değiştirmek istediğiniz markanın kategori adını giriniz: ");
                        kategoriAdı = Console.ReadLine();
                        Console.WriteLine("Değiştirmek istediğiniz markanın ürün adını giriniz: ");
                        urunAdi = Console.ReadLine();
                        Console.WriteLine("Değiştirmek istediğiniz markanın adını giriniz: ");
                        marka = Console.ReadLine();
                        foreach (Urun_agac birAgac in Kategoriler)
                        {
                            if (birAgac.kategoriAdı == kategoriAdı)
                            {
                                if (birAgac.root != null)
                                {
                                    dugum simdiki = birAgac.root;
                                    while (true)
                                    {
                                        if (simdiki.urunAdı == urunAdi)
                                        {
                                            foreach (UrunBilgisi birUrun in simdiki.urunBilgileri)
                                            {
                                                if (birUrun.marka == marka)
                                                {
                                                    Console.WriteLine("Yeni marka adını giriniz: ");
                                                    marka = Console.ReadLine();
                                                    birUrun.marka = marka;
                                                    string[] key = birUrun.urunAcıklaması.Split();
                                                    for (int i = 0; i < key.Length; i++)
                                                    {
                                                        int n;
                                                        bool isNumeric = int.TryParse(key[i], out n);
                                                        if (!isNumeric)
                                                        {
                                                            foreach (UrunBilgisi birUrunBilgisi in (ArrayList)hash[key[i]])
                                                            {
                                                                if (birUrun == birUrunBilgisi)
                                                                    birUrunBilgisi.marka = marka;
                                                            }

                                                        }
                                                    }
                                                    break;
                                                }
                                            }
                                            break;
                                        }
                                        else if (simdiki.urunAdı.CompareTo(urunAdi) == -1)
                                            simdiki = simdiki.rightChild;
                                        else
                                            simdiki = simdiki.leftChild;
                                    }
                                }
                            }
                        }
                        break;
                    case 3:
                        Console.WriteLine("Değiştirmek istediğiniz modelin kategori adını giriniz: ");
                        kategoriAdı = Console.ReadLine();
                        Console.WriteLine("Değiştirmek istediğiniz modelin ürün adını giriniz: ");
                        urunAdi = Console.ReadLine();
                        Console.WriteLine("Değiştirmek istediğiniz modelin adını giriniz: ");
                        model = Console.ReadLine();
                        foreach (Urun_agac birAgac in Kategoriler)
                        {
                            if (birAgac.kategoriAdı == kategoriAdı)
                            {
                                if (birAgac.root != null)
                                {
                                    dugum simdiki = birAgac.root;
                                    while (true)
                                    {
                                        if (simdiki.urunAdı == urunAdi)
                                        {
                                            foreach (UrunBilgisi birUrun in simdiki.urunBilgileri)
                                            {
                                                if (birUrun.model == model)
                                                {
                                                    Console.WriteLine("Yeni model adını giriniz: ");
                                                    model = Console.ReadLine();
                                                    birUrun.model = model;
                                                    string[] key = birUrun.urunAcıklaması.Split();
                                                    for (int i = 0; i < key.Length; i++)
                                                    {
                                                        int n;
                                                        bool isNumeric = int.TryParse(key[i], out n);
                                                        if (!isNumeric)
                                                        {
                                                            foreach (UrunBilgisi birUrunBilgisi in (ArrayList)hash[key[i]])
                                                            {
                                                                if (birUrun == birUrunBilgisi)
                                                                    birUrunBilgisi.model = model;
                                                            }

                                                        }
                                                    }
                                                    break;
                                                }
                                            }
                                            break;
                                        }
                                        else if (simdiki.urunAdı.CompareTo(urunAdi) == -1)
                                            simdiki = simdiki.rightChild;
                                        else
                                            simdiki = simdiki.leftChild;
                                    }
                                }
                            }
                        }
                        break;
                    case 4:
                        Console.WriteLine("Değiştirmek istediğiniz miktarın kategori adını giriniz: ");
                        kategoriAdı = Console.ReadLine();
                        Console.WriteLine("Değiştirmek istediğiniz miktarın ürün adını giriniz: ");
                        urunAdi = Console.ReadLine();
                        Console.WriteLine("Değiştirmek istediğiniz miktarın markasını giriniz: ");
                        marka = Console.ReadLine();
                        foreach (Urun_agac birAgac in Kategoriler)
                        {
                            if (birAgac.kategoriAdı == kategoriAdı)
                            {
                                if (birAgac.root != null)
                                {
                                    dugum simdiki = birAgac.root;
                                    while (true)
                                    {
                                        if (simdiki.urunAdı == urunAdi)
                                        {
                                            foreach (UrunBilgisi birUrun in simdiki.urunBilgileri)
                                            {
                                                if (birUrun.marka == marka)
                                                {
                                                    Console.WriteLine("Yeni miktarı giriniz: ");
                                                    miktar = int.Parse(Console.ReadLine());
                                                    birUrun.miktar = miktar;
                                                    string[] key = birUrun.urunAcıklaması.Split();
                                                    for (int i = 0; i < key.Length; i++)
                                                    {
                                                        int n;
                                                        bool isNumeric = int.TryParse(key[i], out n);
                                                        if (!isNumeric)
                                                        {
                                                            foreach (UrunBilgisi birUrunBilgisi in (ArrayList)hash[key[i]])
                                                            {
                                                                if (birUrun == birUrunBilgisi)
                                                                    birUrunBilgisi.miktar = miktar;
                                                            }

                                                        }
                                                    }
                                                    break;
                                                }
                                            }
                                            break;
                                        }
                                        else if (simdiki.urunAdı.CompareTo(urunAdi) == -1)
                                            simdiki = simdiki.rightChild;
                                        else
                                            simdiki = simdiki.leftChild;
                                    }
                                }
                            }
                        }
                        break;
                    case 5:
                        Console.WriteLine("Değiştirmek istediğiniz fiyatın kategori adını giriniz: ");
                        kategoriAdı = Console.ReadLine();
                        Console.WriteLine("Değiştirmek istediğiniz fiyatın ürün adını giriniz: ");
                        urunAdi = Console.ReadLine();
                        Console.WriteLine("Değiştirmek istediğiniz fiyatın markasını giriniz: ");
                        marka = Console.ReadLine();
                        foreach (Urun_agac birAgac in Kategoriler)
                        {
                            if (birAgac.kategoriAdı == kategoriAdı)
                            {
                                if (birAgac.root != null)
                                {
                                    dugum simdiki = birAgac.root;
                                    while (true)
                                    {
                                        if (simdiki.urunAdı == urunAdi)
                                        {
                                            foreach (UrunBilgisi birUrun in simdiki.urunBilgileri)
                                            {
                                                if (birUrun.marka == marka)
                                                {
                                                    Console.WriteLine("Yeni fiyatı giriniz: ");
                                                    fiyat = int.Parse(Console.ReadLine());
                                                    birUrun.satısFiyatı = fiyat;
                                                    string[] key = birUrun.urunAcıklaması.Split();
                                                    for (int i = 0; i < key.Length; i++)
                                                    {
                                                        int n;
                                                        bool isNumeric = int.TryParse(key[i], out n);
                                                        if (!isNumeric)
                                                        {
                                                            foreach (UrunBilgisi birUrunBilgisi in (ArrayList)hash[key[i]])
                                                            {
                                                                if (birUrun == birUrunBilgisi)
                                                                    birUrunBilgisi.satısFiyatı = fiyat;
                                                            }

                                                        }
                                                    }
                                                    break;
                                                }
                                            }
                                            break;
                                        }
                                        else if (simdiki.urunAdı.CompareTo(urunAdi) == -1)
                                            simdiki = simdiki.rightChild;
                                        else
                                            simdiki = simdiki.leftChild;
                                    }
                                }
                            }
                        }
                        break;
                }
            }
            public void hesapla(dugum localroot, ref double maliyet, ref double satısFiyatı, ref int miktar)
            {
                if (localroot != null)
                {
                    foreach (UrunBilgisi birUrun in localroot.urunBilgileri)
                    {
                        satısFiyatı += birUrun.satısFiyatı;
                        maliyet += birUrun.maliyet;
                        miktar += birUrun.miktar;
                    }
                    hesapla(localroot.leftChild, ref maliyet, ref satısFiyatı, ref miktar);
                    hesapla(localroot.rightChild, ref maliyet, ref satısFiyatı, ref miktar);
                }
            }
            public void adındanUrunArama(string kAdı, string uAdı)
            {
                foreach (Urun_agac birAgac in Kategoriler)
                {
                    if (birAgac.kategoriAdı == kAdı)
                    {
                        dugum simdiki = birAgac.root;
                        while (true)
                        {
                            if (simdiki != null)
                            {
                                if (simdiki.urunAdı.CompareTo(uAdı) == -1)
                                    simdiki = simdiki.rightChild;
                                else if (simdiki.urunAdı.CompareTo(uAdı) == 1)
                                    simdiki = simdiki.leftChild;
                                else
                                {
                                    foreach (UrunBilgisi birUrun in simdiki.urunBilgileri)
                                        Console.WriteLine(birUrun.marka + " markalı " + birUrun.model + " modelinin fiyatı " + birUrun.satısFiyatı);
                                    return;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Girilen verilere ilişkin ürün bulunamadı!");
                                return;
                            }
                        }
                    }
                }
            }
            public void fiyatListele(dugum localroot, int max, int min)
            {
                if (localroot != null)
                {
                    foreach (UrunBilgisi birUrun in localroot.urunBilgileri)
                    {
                        if (birUrun.satısFiyatı < max && birUrun.satısFiyatı > min)
                            Console.WriteLine("Marka: " + birUrun.marka + "\nModel: " + birUrun.model + "\nMiktar: " + birUrun.miktar + "\nMaliyet: " + birUrun.maliyet + "\nSatış Fiyatı: " + birUrun.satısFiyatı + "\nUrun Açıklaması: " + birUrun.urunAcıklaması);
                    }
                    fiyatListele(localroot.leftChild, max, min);
                    fiyatListele(localroot.rightChild, max, min);
                }
            }
            public void kListele(string kAdı)
            {
                foreach (Urun_agac birAgac in Kategoriler)
                {
                    if (birAgac.kategoriAdı == kAdı)
                    {
                        Console.WriteLine("Preorder dolaşma:");
                        birAgac.preOrder(birAgac.root);
                        birAgac.duzey = -1;
                        Console.WriteLine("\nInorder dolaşma:");
                        birAgac.inOrder(birAgac.root);
                        birAgac.duzey = 0;
                        Console.WriteLine("\nPostorder dolaşma:");
                        birAgac.postOrder(birAgac.root);
                        birAgac.duzey = 0;
                        birAgac.derinlik(birAgac.root, -1);
                        Console.WriteLine("\nEleman Sayısı: " + birAgac.elemanSay + "\nDerinlik: " + birAgac.duzey);
                    }
                }
            }
            public void hashOlusturma(dugum birDugum)
            {
                foreach (UrunBilgisi birUrun in birDugum.urunBilgileri)
                {
                    string[] key = birUrun.urunAcıklaması.Split();
                    for (int i = 0; i < key.Length; i++)
                    {
                        int n;
                        bool isNumeric = int.TryParse(key[i], out n);
                        if (!isNumeric)
                        {
                            if (!hash.ContainsKey(key[i]))
                            {
                                ArrayList liste = new ArrayList();
                                liste.Add(birUrun);
                                hash.Add(key[i], liste);
                            }
                            else
                                ((ArrayList)hash[key[i]]).Add(birUrun);
                        }
                    }
                }
            }
            public void siparis()
            {
                string kategoriAdı, urunAdi, marka, model;
                int adet;
                Console.WriteLine("Sipariş etmek istediğiniz ürünün kategori adını giriniz: ");
                kategoriAdı = Console.ReadLine();
                Console.WriteLine("Sipariş etmek istediğiniz ürünün adını giriniz: ");
                urunAdi = Console.ReadLine();
                Console.WriteLine("Sipariş etmek istediğiniz ürünün markasını giriniz: ");
                marka = Console.ReadLine();
                Console.WriteLine("Sipariş etmek istediğiniz ürünün modelini giriniz: ");
                model = Console.ReadLine();
                foreach (Urun_agac birAgac in Kategoriler)
                {
                    if (birAgac.kategoriAdı == kategoriAdı)
                    {
                        if (birAgac.root != null)
                        {
                            dugum simdiki = birAgac.root;
                            while (true)
                            {
                                if (simdiki.urunAdı == urunAdi)
                                {
                                    foreach (UrunBilgisi birUrun in simdiki.urunBilgileri)
                                    {
                                        if (birUrun.marka == marka && birUrun.model == model)
                                        {
                                            Console.WriteLine("Kaç adet ürün sipariş etmek istediğinizi giriniz:");
                                            adet = int.Parse(Console.ReadLine());
                                            birUrun.miktar -= adet;
                                            string[] key = birUrun.urunAcıklaması.Split();
                                            for (int i = 0; i < key.Length; i++)
                                            {
                                                int n;
                                                bool isNumeric = int.TryParse(key[i], out n);
                                                if (!isNumeric)
                                                {
                                                    foreach (UrunBilgisi birUrunBilgisi in (ArrayList)hash[key[i]])
                                                    {
                                                        if (birUrun == birUrunBilgisi)
                                                            birUrunBilgisi.miktar = birUrun.miktar;
                                                    }

                                                }
                                            }
                                            break;
                                        }
                                    }
                                    break;
                                }
                                else if (simdiki.urunAdı.CompareTo(urunAdi) == -1)
                                    simdiki = simdiki.rightChild;
                                else
                                    simdiki = simdiki.leftChild;
                            }
                        }
                    }
                }
            }
            public void hashListele()
            {
                string kelime;
                Console.WriteLine("Aramak istediğiniz kelimeyi giriniz:");
                kelime = Console.ReadLine();
                foreach (UrunBilgisi birUrun in (ArrayList)hash[kelime])
                    Console.WriteLine("Marka: " + birUrun.marka + "\nModel: " + birUrun.model + "\nMiktar: " + birUrun.miktar + "\nMaliyet: " + birUrun.maliyet + "\nSatış Fiyatı: " + birUrun.satısFiyatı + "\nUrun Açıklaması: " + birUrun.urunAcıklaması);
            }
            public void hSilme(dugum localroot, string uAdı, string marka, string model)      // hash ten silme
            {
                if (localroot != null)
                {
                    if (localroot.urunAdı != uAdı)
                    {
                        hSilme(localroot.leftChild, uAdı, marka, model);
                        hSilme(localroot.rightChild, uAdı, marka, model);
                    }
                    else
                    {
                        foreach (UrunBilgisi birUrun in localroot.urunBilgileri)
                        {
                            if (birUrun.marka == marka && birUrun.model == model)
                            {
                                string[] key = birUrun.urunAcıklaması.Split();
                                for (int i = 0; i < key.Length; i++)
                                {
                                    int n;
                                    bool isNumeric = int.TryParse(key[i], out n);
                                    if (!isNumeric)
                                    {
                                        foreach (UrunBilgisi birUrunBilgisi in (ArrayList)hash[key[i]])
                                        {
                                            if (birUrunBilgisi == birUrun)
                                            {
                                                ((ArrayList)hash[key[i]]).Remove(birUrunBilgisi);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            public void heapEkle(dugum localroot, Heap birHeap)
            {
                if (localroot != null)
                {
                    foreach (UrunBilgisi birUrun in localroot.urunBilgileri)
                        birHeap.insert(birUrun);
                    heapEkle(localroot.leftChild, birHeap);
                    heapEkle(localroot.rightChild, birHeap);
                }
            }
            public void guncelleme(dugum localroot, int N, UrunBilgisi urun)
            {
                if (localroot != null && N != 0)
                {
                    foreach (UrunBilgisi birUrun in localroot.urunBilgileri)
                    {
                        if (birUrun == urun)
                        {
                            birUrun.miktar = 0;
                            string[] key = birUrun.urunAcıklaması.Split();
                            for (int i = 0; i < key.Length; i++)
                            {
                                int n;
                                bool isNumeric = int.TryParse(key[i], out n);
                                if (!isNumeric)
                                {
                                    foreach (UrunBilgisi birUrunBilgisi in (ArrayList)hash[key[i]])
                                    {
                                        if (birUrunBilgisi == birUrun)
                                        {
                                            birUrunBilgisi.miktar = 0;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    N -= localroot.urunBilgileri.Count;
                    guncelleme(localroot.leftChild, N, urun);
                    guncelleme(localroot.rightChild, N, urun);
                }
            }
            public void heapListele()
            {
                string kAdı;
                int N;
                UrunBilgisi urun;
                Console.WriteLine("Satın almak istediğiniz ürünün kategorisini giriniz:");
                kAdı = Console.ReadLine();
                foreach (Urun_agac birAgac in Kategoriler)
                {
                    if (kAdı == birAgac.kategoriAdı)
                    {
                        Heap birHeap = new Heap(birAgac.urunSayBul(birAgac.root));
                        heapEkle(birAgac.root, birHeap);
                        Console.WriteLine("Almak istediğiniz ürün adedini giriniz:");
                        N = int.Parse(Console.ReadLine());
                        for (int i = 0; i < N; i++)
                        {
                            urun = birHeap.displayHeap();
                            guncelleme(birAgac.root, N, urun);
                        }
                        break;
                    }
                }
            }
        }
        static public int menuPersonel()
        {
            int secim;
            Console.WriteLine("1. Markete yeni isimde ve/veya kategoride ürün girişi");
            Console.WriteLine("2. Markete, yeni bir marka/modelde ürün girişi");
            Console.WriteLine("3. Adından ürün arama ve silme");
            Console.WriteLine("4. Ürün bilgilerinde değişiklik");
            Console.WriteLine("5. Şirketin toplam gelir, gider ve kârının hesaplanması");
            Console.WriteLine("Yapmak istediğiniz işlemi seçiniz: ");
            return (secim = int.Parse(Console.ReadLine()));
        }
        static public int menuMusteri()
        {
            int secim;
            Console.WriteLine("1. Adından ürün bilgisi arama");
            Console.WriteLine("2. Belirli fiyatlar arasındaki tüm ürünlerin listelenmesi");
            Console.WriteLine("3. Belirtilen bir kategorideki tüm Ürünleri düzeyleri ile birlikte listeleme");
            Console.WriteLine("4. Ürün Siparişi ve Alımı");
            Console.WriteLine("5. Verilen bir kelimenin geçtiği ürünlerin listelenmesi");
            Console.WriteLine("6. Verilen bir kategorideki en ucuz N adet ürünün satın alınması");
            Console.WriteLine("Yapmak istediğiniz işlemi seçiniz: ");
            return (secim = int.Parse(Console.ReadLine()));
        }
        static void Main(string[] args)
        {
            SanalMarket Market = new SanalMarket();
            dugum birDugum;
            UrunBilgisi birUrunBilgisi;
            char cevap, devam;
            int yanıt1, yanıt2, secim, miktar = 0;
            string kategoriAdı, urunAdı, marka, model;
            double maliyet = 0, satısFiyatı = 0;
            do
            {
                Console.WriteLine("1. Personel girişi\n2. Müşteri girişi");
                secim = int.Parse(Console.ReadLine());
                if (secim == 1)
                {
                    yanıt1 = menuPersonel();
                    switch (yanıt1)
                    {
                        case 1:
                            birDugum = new dugum();
                            Urun_agac Agac = new Urun_agac();
                            Console.WriteLine("Eklemek istediğiniz ürünün kategorisini giriniz:");
                            Agac.kategoriAdı = Console.ReadLine();
                            Console.WriteLine("Eklemek istediğiniz ürünün adını giriniz:");
                            birDugum.urunAdı = Console.ReadLine();
                            do
                            {
                                birUrunBilgisi = new UrunBilgisi();
                                Console.WriteLine("Eklemek istediğiniz ürünün bilgilerini giriniz:");
                                Console.WriteLine("Marka:");
                                birUrunBilgisi.marka = Console.ReadLine();
                                Console.WriteLine("Model:");
                                birUrunBilgisi.model = Console.ReadLine();
                                Console.WriteLine("Miktar:");
                                birUrunBilgisi.miktar = int.Parse(Console.ReadLine());
                                Console.WriteLine("Maliyet:");
                                birUrunBilgisi.maliyet = double.Parse(Console.ReadLine());
                                Console.WriteLine("Satış Fiyatı:");
                                birUrunBilgisi.satısFiyatı = double.Parse(Console.ReadLine());
                                Console.WriteLine("Urun Açıklaması:");
                                birUrunBilgisi.urunAcıklaması = Console.ReadLine();
                                birDugum.uBilgiEkle(birUrunBilgisi);
                                Console.WriteLine("Urun bilgileri eklemeye devam etmek istiyor musunuz(e/E/h/H)?: ");
                                devam = char.Parse(Console.ReadLine());
                            } while (devam == 'e' || devam == 'E');
                            Agac.urunEkle(birDugum);
                            Market.hashOlusturma(birDugum);
                            Market.kategoriEkle(Agac);
                            break;
                        case 2:
                            Console.WriteLine("Eklemek istediğiniz ürünün kategorisini giriniz:");
                            kategoriAdı = Console.ReadLine();
                            foreach (Urun_agac birAgac in Market.Kategoriler)
                            {
                                if (birAgac.kategoriAdı == kategoriAdı)
                                {
                                    Console.WriteLine("Eklemek istediğiniz ürünün adını giriniz:");
                                    urunAdı = Console.ReadLine();
                                    dugum simdiki = birAgac.root;
                                    while (true)
                                    {
                                        if (simdiki != null)
                                        {
                                            if (simdiki.urunAdı.CompareTo(urunAdı) == -1)
                                                simdiki = simdiki.rightChild;
                                            else if (simdiki.urunAdı.CompareTo(urunAdı) == 1)
                                                simdiki = simdiki.leftChild;
                                            else
                                            {
                                                birUrunBilgisi = new UrunBilgisi();
                                                Console.WriteLine("Eklemek istediğiniz ürünün bilgilerini giriniz:");
                                                Console.WriteLine("Marka:");
                                                birUrunBilgisi.marka = Console.ReadLine();
                                                Console.WriteLine("Model:");
                                                birUrunBilgisi.model = Console.ReadLine();
                                                Console.WriteLine("Miktar:");
                                                birUrunBilgisi.miktar = int.Parse(Console.ReadLine());
                                                Console.WriteLine("Maliyet:");
                                                birUrunBilgisi.maliyet = double.Parse(Console.ReadLine());
                                                Console.WriteLine("Satış Fiyatı:");
                                                birUrunBilgisi.satısFiyatı = double.Parse(Console.ReadLine());
                                                Console.WriteLine("Urun Açıklaması:");
                                                birUrunBilgisi.urunAcıklaması = Console.ReadLine();
                                                simdiki.uBilgiEkle(birUrunBilgisi);
                                                string[] str = birUrunBilgisi.urunAcıklaması.Split();
                                                for (int i = 0; i < str.Length; i++)
                                                {
                                                    int n;
                                                    bool isNumeric = int.TryParse(str[i], out n);
                                                    if (!isNumeric)
                                                    {
                                                        if (!Market.hash.ContainsKey(str[i]))
                                                        {
                                                            ArrayList liste = new ArrayList();
                                                            liste.Add(birUrunBilgisi);
                                                            Market.hash.Add(str[i], liste);
                                                        }
                                                        else
                                                            ((ArrayList)Market.hash[str[i]]).Add(birUrunBilgisi);
                                                    }
                                                }
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                            break;
                        case 3:
                            Console.WriteLine("Silmek istediğiniz ürünün kategorisini giriniz:");
                            kategoriAdı = Console.ReadLine();
                            Console.WriteLine("Silmek istediğiniz ürünün adını giriniz:");
                            urunAdı = Console.ReadLine();
                            foreach (Urun_agac birAgac in Market.Kategoriler)
                            {
                                if (birAgac.kategoriAdı == kategoriAdı)
                                {
                                    Console.WriteLine("Silmek istediğiniz ürünün markasını giriniz:");
                                    marka = Console.ReadLine();
                                    Console.WriteLine("Silmek istediğiniz ürünün modelini giriniz:");
                                    model = Console.ReadLine();
                                    Market.hSilme(birAgac.root, urunAdı, marka, model);
                                    birAgac.urunAramaSilme(birAgac.root, urunAdı, marka, model);
                                    break;
                                }
                            }
                            break;
                        case 4:
                            Market.uBilgiDegistir();
                            break;
                        case 5:
                            foreach (Urun_agac birAgac in Market.Kategoriler)
                                Market.hesapla(birAgac.root, ref maliyet, ref satısFiyatı, ref miktar);
                            Console.WriteLine("Şirketin toplam geliri: " + satısFiyatı * miktar);
                            Console.WriteLine("Şirketin toplam gideri: " + maliyet * miktar);
                            Console.WriteLine("Şirketin toplam kârı: " + (satısFiyatı - maliyet) * miktar);
                            break;
                    }
                }                     // personel girişi
                else
                {
                    yanıt2 = menuMusteri();
                    switch (yanıt2)
                    {
                        case 1:
                            Console.WriteLine("Aramak istediğiniz ürünün kategori adını giriniz:");
                            kategoriAdı = Console.ReadLine();
                            Console.WriteLine("Aramak istediğiniz ürünün ürün adını giriniz:");
                            urunAdı = Console.ReadLine();
                            Market.adındanUrunArama(kategoriAdı, urunAdı);
                            break;
                        case 2:
                            int max, min;
                            Console.WriteLine("Maximum fiyatı giriniz:");
                            max = int.Parse(Console.ReadLine());
                            Console.WriteLine("Minimum fiyatı giriniz:");
                            min = int.Parse(Console.ReadLine());
                            foreach (Urun_agac birAgac in Market.Kategoriler)
                                Market.fiyatListele(birAgac.root, max, min);
                            break;
                        case 3:
                            Console.WriteLine("Listelemek istediğiniz kategorinin adını giriniz:");
                            kategoriAdı = Console.ReadLine();
                            Market.kListele(kategoriAdı);
                            break;
                        case 4:
                            Market.siparis();
                            break;
                        case 5:
                            Market.hashListele();
                            break;
                        case 6:
                            Market.heapListele();
                            break;
                    }
                }                    // müşteri girişi
                Console.WriteLine("İşlemlere devam etmek istiyor musunuz(e/E/h/H)?: ");
                cevap = char.Parse(Console.ReadLine());
            } while (cevap == 'e' || cevap == 'E');
            Console.ReadKey();
        }
    }
}