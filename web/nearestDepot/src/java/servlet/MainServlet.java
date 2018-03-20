/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package servlet;

import java.io.ByteArrayInputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.PrintWriter;
import java.nio.charset.StandardCharsets;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Enumeration;
import java.util.HashMap;
import java.util.Iterator;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.xml.parsers.*;
import net.minidev.json.JSONObject;
import org.w3c.dom.Document;
import util.ConnectionManager;
import util.verifyAgainstXSD;

/**
 *
 * @author yk
 */
public class MainServlet extends HttpServlet {
    boolean isInitiate = true;
    /**
     * Processes requests for both HTTP <code>GET</code> and <code>POST</code>
     * methods.
     *
     * @param request servlet request
     * @param response servlet response
     * @throws ServletException if a servlet-specific error occurs
     * @throws IOException if an I/O error occurs
     */
    protected void processRequest(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        
        final String workDir = "C:\\EI\\busDepot\\";
        
        InputStream xml = null;
        InputStream xsd = new FileInputStream(new File(workDir+"sample.xsd"));
        JSONObject json = new JSONObject();
        response.setContentType("application/json");
        try (PrintWriter out = response.getWriter()) {
            Enumeration<String> e = request.getParameterNames();
            String stringValue = "";
            while(e.hasMoreElements()){
                String s = e.nextElement();
                stringValue = request.getParameter(s);
                xml = new ByteArrayInputStream(stringValue.getBytes(StandardCharsets.UTF_8));
                System.out.println(s + ": " +" : "+(request.getParameter(s) instanceof String) + request.getParameter(s));
            }
            
            if(verifyAgainstXSD.verify(xml, xsd)){
                System.out.println("VERIFIED");
                xml.close();
                xsd.close();
                xml = new ByteArrayInputStream(stringValue.getBytes(StandardCharsets.UTF_8));
                
                DocumentBuilderFactory dbFactory = DocumentBuilderFactory.newInstance();
                DocumentBuilder dBuilder = dbFactory.newDocumentBuilder();
                Document doc = dBuilder.parse(xml);
                doc.getDocumentElement().normalize();
                String dataString =  doc.getDocumentElement().getElementsByTagName("coordinates").item(0).getChildNodes().item(0).getNodeValue();
                String[] data = dataString.split(",");
                double lattitude = Double.parseDouble(data[0]);
                double longitude = Double.parseDouble(data[1]);
                
                xml.close();
                
                HashMap<Double[],String> idMap = new HashMap<>();
                ArrayList<Double[]> depots = getDepotCoordinates(idMap);
                
                Iterator it = idMap.keySet().iterator();
                
                while(it.hasNext()){
                    System.out.println(idMap.get(it.next()));
                }
                double minDist = Double.MAX_VALUE;
                String ID = "";
                for(Double[] d : depots){
                    double currDist = distance(lattitude, d[0],longitude, d[1]);
                    if(currDist < minDist){
                        minDist = currDist;
                        ID = idMap.get(d);
                    }
                }
                
                json.put("message", ID);
                json.put("status","success");
                out.write(json.toJSONString());
                System.out.println(minDist + " : " + ID);
            }
            else{
               //todo: SETTLE ERROR HANDLING
                json.put("message", "");
                json.put("status","fail");
                json.put("error","XML schema validation failure");
                out.write(json.toJSONString());
                System.out.println("NOT VERIFIED");
               
            }
            
            
            
        }catch(Exception e){
        }
    }
    
    ArrayList<Double[]> getDepotCoordinates(HashMap<Double[],String> hash){
        try {
            Connection conn = ConnectionManager.getConnection();
            String stmt = "select * from coordinates";
            PreparedStatement query = conn.prepareStatement(stmt);
            ResultSet rs = query.executeQuery();
            ArrayList<Double[]> ls = new ArrayList<>();
            
            while (rs.next()) {
                String id = rs.getString("ID");
                double lattitude = rs.getDouble("latitude");
                double longitude = rs.getDouble("longitude");
                Double[] d = {lattitude,longitude};
                hash.put(d, id);
                
                ls.add(d);
            }
            return ls;
            
        } catch (SQLException ex) {
            Logger.getLogger(MainServlet.class.getName()).log(Level.SEVERE, null, ex);
        }
        return null;
    }
    
    double distance(double lat1, double lat2, double lon1,
        double lon2) {

    final int R = 6371; // Radius of the earth

    double latDistance = Math.toRadians(lat2 - lat1);
    double lonDistance = Math.toRadians(lon2 - lon1);
    double a = Math.sin(latDistance / 2) * Math.sin(latDistance / 2)
            + Math.cos(Math.toRadians(lat1)) * Math.cos(Math.toRadians(lat2))
            * Math.sin(lonDistance / 2) * Math.sin(lonDistance / 2);
    double c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    double distance = R * c * 1000; // convert to meters

    double height = 0;

    distance = Math.pow(distance, 2) + Math.pow(height, 2);

    return Math.sqrt(distance);
}

    // <editor-fold defaultstate="collapsed" desc="HttpServlet methods. Click on the + sign on the left to edit the code.">
    /**
     * Handles the HTTP <code>GET</code> method.
     *
     * @param request servlet request
     * @param response servlet response
     * @throws ServletException if a servlet-specific error occurs
     * @throws IOException if an I/O error occurs
     */
    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        processRequest(request, response);
    }

    /**
     * Handles the HTTP <code>POST</code> method.
     *
     * @param request servlet request
     * @param response servlet response
     * @throws ServletException if a servlet-specific error occurs
     * @throws IOException if an I/O error occurs
     */
    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        processRequest(request, response);
    }

    /**
     * Returns a short description of the servlet.
     *
     * @return a String containing servlet description
     */
    @Override
    public String getServletInfo() {
        return "Short description";
    }// </editor-fold>

}
