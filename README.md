# Fitness App

Bu bana gönderdiğiniz dökümandaki fitness uygulamasının kodlarıdır. Uygulamada users/register end pointine bir username ve password ile hesap oluşturduktan sonra, users/login endpointinden uygulamaya giriş yapabilir, hareket ve antreman ekleyip silebilirsiniz.
Uygulamada değişken isimlerini ingilizce olarak kullandım. USer -> Kullanıcı, Movmeent -> Hareket, Workout -> Antreman şeklinde düşünebilirsiniz.

## Database
Antreman ve Hareketler arasında many-to-many bir ilişki olacak şekilde dizayn ettimç

## Teknolojiler:
* .Net Core
* MySQL
* JWT
* N-Layer
* Repository Pattern

## Kurulum

* GitHubdan kodları indirin.
* Tüm  bağımlılıkları yüklemek için 'dotnet restore'yi çalıştırın.
* MySQL kullanıcı bilgilerinizle uygulamayı bağlayın.
* SQLScripts klasöründeki data.sql i çalıştırın
* SQLScripts klasöründeki proc.sql dosyasındaki stored procedureları kendi veritabanınıza alın.
* Uygulamayı debug modda ayağa kaldırdığınızda swagger sayfasını göreceksiniz. 

## Enpoints
Yaratmaya karar verdim:
* `/users`: Authenticationdan sorumlu. Bunu ayırmak daha doğru olabilirdir. Bunun dışında genel user CRUP işlemleri için geliştirilebilir.
* `/movements` -> hareketlerden sorumlu. Http Kodları ile CRUD işlemlerini destekler. Authorization zorunlu.
* `/workouts` -> antremanlardan sorumlu. Http methodlarıyla CRUD işlemlerini destkler. 

