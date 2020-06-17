# Getting Started with Telerik TagIt Sample Application

To get you started with Telerik TagIt, our Xamarin.Forms sample application for content management, first you need to complete the following steps:

Step #1: Install [Progress® Telerik® UI for Xamarin controls](https://www.telerik.com/xamarin-ui) 

Step #2: Get the source code of the application 

# Telerik Tagit

Telerik Tagit is a cross-platform native mobile application designed to turn the photo collection on your phone into a database that you can search and sort by the content contained in the individual images.

It uses the [Progress® Telerik® UI for Xamarin controls](https://www.telerik.com/xamarin-ui) for the front end, offering a stunning, high-performant UI, and [Microsoft Azure's Computer Vision](https://azure.microsoft.com/en-us/services/cognitive-services/computer-vision/) API on the back end to caption images and tag them with search keywords. The app is available for Android, iOS and UWP.

![Telerik Tagit](https://lh3.googleusercontent.com/iATgRrRjwcUAyrmeHCGFpqKTl8M3_Z-cJzb8-Xi2ER516IpLcKR-v3awnlMg_Jvn_Mea=w250-h2544-rw) ![Telerik Tagit](https://lh3.googleusercontent.com/T5UINOJW9oHoPxv-OKAcoDGu0hpb8SKvjsnjwS1TBpICHFwuUrwvtsll-9IcOsHRVeY=w250-h2544-rw) ![Telerik Tagit](https://lh3.googleusercontent.com/wyM9EI1_ej7cymc31_HnR4fgd5bIMvMhWiUiBGSFwoapU2C8UHY-HeCgGe_TtnwBLCK2=w250-h1200-rw) 

## Prerequisites before building the application

In order to be able to able to build the application:

* [Progress® Telerik® UI for Xamarin controls](https://www.telerik.com/xamarin-ui) need to be installed.
* [Microsoft Azure's Computer Vision](https://azure.microsoft.com/en-us/services/cognitive-services/computer-vision/) service needs to be created.

### UI for Xamarin controls installation

The stunning and high-performant UI of the application is built using the UI for Xamarin controls provided by Telerik. Before building the application an installation of the controls is needed. In the [official documentation](https://docs.telerik.com/devtools/xamarin/installation-and-deployment/download-product-files) of Progress® Telerik® UI for Xamarin you can find a detailed information how to install the controls both on Windows and MacOS. After the installation is finished the referenced by the solution binaries will automatically be resolved.

If you prefer to use nuget packages instead of binary references Telerik provides a nuget server. The UI for Xamarin nuget package could be installed from it. [Here](https://docs.telerik.com/devtools/xamarin/installation-and-deployment/telerik-nuget-server) you can find detailed information about the Telerik nuget server and how you can setup it. Please, be aware that in order to use the nuget server a [registration](https://docs.telerik.com/devtools/xamarin/installation-and-deployment/download-product-files) is required.

### Microsoft Azure's Computer Vision

The main functionality of the application for tagging the images is implemented using the [Microsoft Azure's Computer Vision](https://azure.microsoft.com/en-us/services/cognitive-services/computer-vision/) service. Before building the application you need to create such a service using the steps below.

1. Go to [https://azure.microsoft.com/en-us/services/cognitive-services/computer-vision/](https://azure.microsoft.com/en-us/services/cognitive-services/computer-vision/). You need to have an Azure account in order to be able to create a service.
2. Once you have logged-in go to the [portal page](https://portal.azure.com/#home) of Azure.
3. In the portal page select the **Cognitive services** category.
4. After you are navigated to the Cognitive services page press the **Add button**.
5. A new page will be visualized from where you can select to create a **Computer visual** service. If you don't see that particular category you can use the search option of the page to find it.
6. Click on Computer vision and after that press create.
7. In the visualized page fill-in all the required information and after that press the **Create button**.
8. After the service is successfully created copy the location region (eastus, westus etc.). Go to the **["CoreConstants.cs"](https://github.com/telerik/telerik-xamarin-forms-samples/blob/master/TagIt/tagit/tagit/Common/CoreConstants.cs)** file and set the copied location to the **"CognitiveServicesRegion"** const.
9. Go back to the service and copy the **Subscription ID** (Key1 in most cases) and paste it as a value for the **"ComputerVisionApiSubscriptionKey"** const.

Only if all the required binaries from the installation are referenced and the service is setup correctly the application will build and run as expected.

For more information on the application and a walk through of how it was built, please refer to the [Telerik Tagit page](https://www.telerik.com/xamarin-ui/telerik-tagit).

## Download Tagit

If you want to check the application now you can use the following links and download it from the stores: 
* [Telerik Tagit on AppStore](https://apps.apple.com/us/app/telerik-tagit/id1310584457)
* [Telerik Tagit on Google Play](https://play.google.com/store/apps/details?id=com.telerik.tagit)
* [Telerik Tagit on Windows Store](https://www.microsoft.com/en-us/store/p/telerik-tagit/9pb07plrwpfs)
