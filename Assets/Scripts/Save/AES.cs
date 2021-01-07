using UnityEngine;
using System.Collections;
using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;

class AES
{
    private RijndaelManaged myRijndael;

    public AES(string key, string ivValue)
    {
        myRijndael = new RijndaelManaged
        {
            Key = Encoding.ASCII.GetBytes(key),
            IV = Encoding.ASCII.GetBytes(ivValue)
        };
    }

    public string Decrypt(byte[] soup)
    {
        string outString = "";

        try
        {
            outString = DecryptStringFromBytes(soup, myRijndael);
        }
        catch (Exception e)
        {
            Debug.LogFormat("Error: {0}", e.Message);
        }

        return outString;
    }

    public byte[] Encrypt(string original)
    {
        byte[] encrypted = null;

        try
        {
            encrypted = EncryptStringToBytes(original, myRijndael);
        }
        catch (Exception e)
        {
            Debug.LogFormat("Error: {0}", e.Message);
        }

        return encrypted;
    }

    static byte[] EncryptStringToBytes(string plainText, RijndaelManaged myRijndael)
    {
        // Check arguments
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException("plainText");
        if (myRijndael.Key == null || myRijndael.Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (myRijndael.IV == null || myRijndael.IV.Length <= 0)
            throw new ArgumentNullException("IV");

        byte[] encrypted;

        // Create a decrytor to perform the stream transform
        ICryptoTransform encryptor = myRijndael.CreateEncryptor(myRijndael.Key, myRijndael.IV);

        // Create the streams used for encryption
        using (MemoryStream msEncrypt = new MemoryStream())
        {
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    //Write all data to the stream
                    swEncrypt.Write(plainText);
                }
                encrypted = msEncrypt.ToArray();
            }
        }

        // Return the encrypted bytes from the memory stream
        return encrypted;
    }

    static string DecryptStringFromBytes(byte[] cipherText, RijndaelManaged myRijndael)
    {
        // Check arguments
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException("cipherText");
        if (myRijndael.Key == null || myRijndael.Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (myRijndael.IV == null || myRijndael.IV.Length <= 0)
            throw new ArgumentNullException("IV");

        // Declare the string used to hold decrypted text
        string plaintext = null;

        // Create a decrytor to perform the stream transform
        ICryptoTransform decryptor = myRijndael.CreateDecryptor(myRijndael.Key, myRijndael.IV);

        // Create the streams used for decryption
        using (MemoryStream msDecrypt = new MemoryStream(cipherText))
        {
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            {
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {

                    // Read the decrypted bytes from the decrypting stream and place them in a string
                    plaintext = srDecrypt.ReadToEnd();
                }
            }
        }

        return plaintext;
    }
}
